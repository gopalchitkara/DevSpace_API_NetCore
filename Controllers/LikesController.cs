using AutoMapper;
using DevSpace_API.Data.Likes;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevSpace_API.Controllers
{
    [ApiController]
    [Route("api/likes")]
    public class LikesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILikeRepo _repository;

        public LikesController(ILikeRepo repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpPost("addlike")]
        public ActionResult AddLike(CreateLikeDto dto)
        {
            var result = _repository.GetLikeByUserAndArticleId(dto.UserId, dto.ArticleId);

            if (result != null)
            {
                return NoContent();
            }

            _repository.AddLike(_mapper.Map<Like>(dto));
            return Ok();
        }

        [HttpPost("removelike")]
        public ActionResult RemoveLike(CreateLikeDto dto)
        {
            var result = _repository.GetLikeByUserAndArticleId(dto.UserId, dto.ArticleId);

            if (result == null)
            {
                return NoContent();
            }

            _repository.RemoveLike(_mapper.Map<Like>(dto));
            return Ok();
        }
    }
}