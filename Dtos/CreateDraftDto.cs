using System.Collections.Generic;

namespace DevSpace_API.Dtos
{
    public class CreateDraftDto
    {
        public string Title { get; set; }
        public IEnumerable<string> Hashtags { get; set; }
        public string Body { get; set; }
        public string AuthorId { get; set; }
    }
}