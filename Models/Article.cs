using System;
using System.Collections.Generic;

namespace DevSpace_API.Models
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public ICollection<Hashtag> Hashtags { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AuthorId { get; set; }
        public string AuthorUsername { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}