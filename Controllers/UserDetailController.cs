using AutoMapper;
using DevSpace_API.Data.Userdetails;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSpace_API.Controllers
{
    [ApiController]
    [Route("api/userdetail")]
    public class UserDetailController : ControllerBase
    {
        private readonly IUserDetailRepo _repository;
        private readonly IMapper _mapper;

        public UserDetailController(IUserDetailRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{userid}")]
        public ActionResult<UserDetail> GetUserDetailByUserid(string userid)
        {
            if (userid != null && userid != "")
            {
                return Ok(_repository.GetUserDetailByUserid(userid));
            }
            return NotFound();
        }

        [HttpGet("GetUserDetailByUsername/{username}")]
        public ActionResult<UserDetail> GetUserDetailByUsername(string username)
        {
            if (username != null && username != "")
            {
                return Ok(_repository.GetUserDetailByUsername(username));
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost("UpdateUserDetail/{userid}")]
        public ActionResult UpdateUserDetail(string userid, [FromBody] UpdateUserDetailDto updateUserDetailDto)
        {
            var userDetailModelFromRepo = _repository.GetUserDetailByUserid(userid);
            if (userDetailModelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(updateUserDetailDto, userDetailModelFromRepo);
            _repository.UpdateUserDetail(userDetailModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}