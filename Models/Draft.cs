using System;
using System.Collections.Generic;

namespace DevSpace_API.Models
{
    public class Draft
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public ICollection<DraftHashtag> Hashtags { get; set; }
        public string Body { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}