using System.Collections.Generic;
using DevSpace_API.Models;

namespace DevSpace_API.Dtos
{
    public class ReadDraftDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<DraftHashtag> Hashtags { get; set; }
        public string Body { get; set; }
        public string AuthorId { get; set; }
    }
}