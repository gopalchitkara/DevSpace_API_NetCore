using System.Threading.Tasks;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using DevSpace_API.Services;
using DevSpace_API.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSpace_API.Controllers
{
    [ApiController]
    [Route("/api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // api/auth/register
        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] SignUpModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result.Message);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result.Message);
            }

            return BadRequest("Some properties are not valid");
        }
    }
}