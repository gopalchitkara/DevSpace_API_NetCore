using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevSpace_API.Data.Article;
using DevSpace_API.Data.ReadingList;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSpace_API.Controllers
{
    [ApiController]
    [Route("/api/readinglists")]
    [Authorize]
    public class ReadingListsController : ControllerBase
    {
        private readonly IReadingListRepo _repository;
        private readonly IMapper _mapper;

        public ReadingListsController(IReadingListRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{userId}")]
        public ActionResult<IEnumerable<Models.Article>> GetSavedArticlesByUserId(string userId)
        {
            var savedArticles = _repository.GetSavedArticlesByUserId(userId);

            if (savedArticles != null && savedArticles.Count() > 0)
            {
                // return Ok(_mapper.Map<IEnumerable<ReadArticleDto>>(savedArticles));
                var result = _mapper.Map<IEnumerable<FeedArticleDto>>(savedArticles).ToList();
                result.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
                return Ok(result);
            }
            return NoContent();
        }

        [HttpPost]
        [Route("saveArticle")]
        public ActionResult SaveArticleForUserId(ReadingListArticleDto dto)
        {
            try
            {
                _repository.SaveArticleForUserId(dto.UserId, dto.ArticleId);
                _repository.SaveChanges();
            }
            catch (System.Exception)
            {
                return BadRequest("something went wrong");
            }
            return NoContent();
        }

        [HttpPost]
        [Route("unsaveArticle")]
        public ActionResult UnSaveArticleForUserId(ReadingListArticleDto dto)
        {
            try
            {
                _repository.UnSaveArticleForUserId(dto.UserId, dto.ArticleId);
                _repository.SaveChanges();
            }
            catch (System.Exception)
            {
                return BadRequest("something went wrong");
            }
            return NoContent();
        }
    }
}