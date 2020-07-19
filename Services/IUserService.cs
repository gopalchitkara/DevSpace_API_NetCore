using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DevSpace_API.DataContext;
using DevSpace_API.Models;
using DevSpace_API.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevSpace_API.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(SignUpModel model);
        Task<UserManagerResponse> LoginUserAsync(SignInModel model);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, ApplicationContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task<UserManagerResponse> LoginUserAsync(SignInModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Sign In model is null");
            }

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with that Username.",
                    IsSuccess = false
                };
            }

            var loginResult = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!loginResult)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid Password.",
                    IsSuccess = false
                };
            }

            var claims = new[]
            {
                new Claim("Username", model.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            var userDetails = _context.UserDetails.FirstOrDefault(u => u.UserId == user.Id);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                UserDetails = new
                {
                    id = user.Id,
                    username = user.UserName,
                    firstName = userDetails.FirstName,
                    lastName = userDetails.LastName
                }
            };
        }

        public async Task<UserManagerResponse> RegisterUserAsync(SignUpModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Sign Up model is null");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match password.",
                    IsSuccess = false
                };
            }

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Username
            };

            var userSignUpResult = await _userManager.CreateAsync(identityUser, model.Password);

            if (userSignUpResult.Succeeded)
            {
                var newlyCreatedUser = await _userManager.FindByNameAsync(model.Username);

                var newUserDetails = new UserDetail
                {
                    UserId = newlyCreatedUser.Id,
                    Username = model.Username,
                    FirstName = model.FullName.Split(" ")[0],
                    LastName = model.FullName.Split(" ")[1],
                    EmailId = model.Email,
                    JoinedOn = DateTime.Now
                };

                _context.UserDetails.Add(newUserDetails);
                _context.SaveChanges();

                var userSignInModel = new SignInModel
                {
                    Username = model.Username,
                    Password = model.Password
                };
                return await LoginUserAsync(userSignInModel);
            }

            return new UserManagerResponse
            {
                Message = "User did not create.",
                IsSuccess = false,
                Errors = userSignUpResult.Errors.Select(e => e.Description)
            };
        }
    }
}