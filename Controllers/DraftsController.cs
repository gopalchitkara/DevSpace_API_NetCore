using System;
using System.Collections.Generic;
using AutoMapper;
using DevSpace_API.Data.Drafts;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSpace_API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("/api/drafts")]
    public class DraftsController : ControllerBase
    {
        private readonly IDraftRepo _repository;
        private readonly IMapper _mapper;

        public DraftsController(IDraftRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("GetDraftsByUserId/{userid}")]
        public ActionResult<IEnumerable<ReadDraftDto>> GetDraftsByUserId(string userid)
        {
            if (userid.Length <= 0)
                return BadRequest();

            var userDraftsFromRepo = _repository.GetDraftsByUserId(userid);

            if (userDraftsFromRepo != null)
            {
                return Ok(_mapper.Map<IEnumerable<ReadDraftDto>>(userDraftsFromRepo));
            }

            return NotFound();
        }

        [HttpGet("GetDraftById/{id}", Name = "GetDraftById")]
        public ActionResult<ReadDraftDto> GetDraftById(long id)
        {
            if (id <= 0)
                return BadRequest();

            var draft = _repository.GetDraftById(id);

            if (draft != null)
            {
                return Ok(_mapper.Map<ReadDraftDto>(draft));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateDraft([FromBody] CreateDraftDto createDraftDto)
        {
            var draftModel = new Draft
            {
                Title = createDraftDto.Title,
                Body = createDraftDto.Body,
                AuthorId = createDraftDto.AuthorId,
                CreatedAt = DateTime.Now
            };

            _repository.CreateDraft(draftModel);
            _repository.SaveChanges();

            List<DraftHashtag> draftHashtags = new List<DraftHashtag>();

            foreach (var item in createDraftDto.Hashtags)
            {
                draftHashtags.Add(new DraftHashtag { Description = item, DraftId = draftModel.Id });
            }

            _repository.AddHashtagsForNewDraft(draftHashtags);
            _repository.SaveChanges();

            var readDraftDto = _mapper.Map<ReadDraftDto>(draftModel);

            return CreatedAtRoute(nameof(GetDraftById), new { Id = readDraftDto.Id }, readDraftDto);
        }

        [HttpPut("{draftid}")]
        public ActionResult UpdateDraft(long draftid, [FromBody] UpdateDraftDto updateDraftDto)
        {
            var draftModelFromRepo = _repository.GetDraftById(draftid);

            if (draftModelFromRepo == null)
                return NotFound();

            _mapper.Map(updateDraftDto, draftModelFromRepo);
            _repository.UpdateDraft(draftModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{draftid}")]
        public ActionResult DeleteDraft(long draftid)
        {
            var draftModelFromRepo = _repository.GetDraftById(draftid);

            if (draftModelFromRepo == null)
                return NotFound();

            _repository.DeleteDraft(draftModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

    }
}