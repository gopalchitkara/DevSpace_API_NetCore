using System;
using System.Collections.Generic;
using DevSpace_API.Models;

namespace DevSpace_API.Dtos
{
    public class ReadArticleDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Hashtag> Hashtags { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AuthorUsername { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}