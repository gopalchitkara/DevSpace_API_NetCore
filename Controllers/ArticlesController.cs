using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevSpace_API.Data.Article;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSpace_API.Controllers
{

    [ApiController]
    [Route("/api/articles")]
    // [Authorize]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepo _repository;
        private readonly IMapper _mapper;

        public ArticlesController(IArticleRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //Get /api/articles
        [HttpGet]
        public ActionResult<IEnumerable<ReadArticleDto>> GetAllarticles()
        {
            var articles = _repository.GetAllArticles();

            if (articles != null)
            {
                return Ok(_mapper.Map<IEnumerable<ReadArticleDto>>(articles));
            }
            return NotFound();
        }

        [HttpGet("feedArticles")]
        public ActionResult<IEnumerable<FeedArticleDto>> GetFeedArticles()
        {
            var articles = _repository.GetAllArticles();

            if (articles != null)
            {
                var result = _mapper.Map<IEnumerable<FeedArticleDto>>(articles).ToList();
                result.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("{id}", Name = "GetArticleById")]
        public ActionResult<ReadArticleDto> GetArticleById(long id)
        {
            var article = _repository.GetArticleById(id);

            if (article != null)
            {
                return Ok(_mapper.Map<ReadArticleDto>(article));
            }
            return NotFound();
        }

        [HttpGet("GetArticlesByTopic/{topicName}")]
        public ActionResult<IEnumerable<FeedArticleDto>> GetArticlesByTopic(string topicName)
        {
            var articles = _repository.GetArticlesByTopic(topicName);

            if (topicName != null || topicName.Length > 0)
            {
                var result = _mapper.Map<IEnumerable<FeedArticleDto>>(articles).ToList();
                result.Sort((x, y) => y.CreatedAt.CompareTo(x.CreatedAt));
                return Ok(result.Take(4));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<ReadArticleDto> CreateArticle(CreateArticleDto createArticleDto)
        {
            List<Hashtag> hashtagList = new List<Hashtag>();

            Article articleModel = new Article
            {
                Title = createArticleDto.Title,
                Body = createArticleDto.Body,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                AuthorId = createArticleDto.AuthorId,
                AuthorUsername = createArticleDto.AuthorUsername,
            };
            _repository.CreateArticle(articleModel);
            _repository.SaveChanges();

            foreach (var item in createArticleDto.Hashtags)
            {
                hashtagList.Add(new Hashtag { Description = item, ArticleId = articleModel.Id });
            }
            _repository.AddHashtagsForNewArticle(hashtagList);
            _repository.SaveChanges();

            var readArticleDto = _mapper.Map<ReadArticleDto>(articleModel);

            return CreatedAtRoute(nameof(GetArticleById), new { Id = readArticleDto.Id }, readArticleDto);
        }
    }
}