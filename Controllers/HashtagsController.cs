using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DevSpace_API.Data.Hashtags;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevSpace_API.Controllers
{
    [ApiController]
    [Route("/api/hashtags")]
    public class HashtagsController : ControllerBase
    {
        private readonly IHashtagRepo _repository;
        private readonly IMapper _mapper;

        public HashtagsController(IHashtagRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetHashtagsForWidgets")]
        public ActionResult<IEnumerable<ReadHashtagForWidgetDto>> GetHashtagsForWidgets()
        {
            var hashtagForWidgets = _repository.GetHashtagsForWidgets();

            if (hashtagForWidgets != null)
            {
                var result = _mapper.Map<IEnumerable<ReadHashtagForWidgetDto>>(hashtagForWidgets).ToList();
                //sort hashtags based on rank
                result.Sort((x, y) => x.Rank.CompareTo(y.Rank));
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("GetHashtagSuggestions/{searchString}")]
        public ActionResult<IEnumerable<string>> GetHashtagSuggestions(string searchString)
        {
            return Ok(_repository.GetHashtagSuggestions(searchString));
        }
    }
}