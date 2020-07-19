using System;
using System.Collections.Generic;
using DevSpace_API.Models;

namespace DevSpace_API.Dtos
{
    public class FeedArticleDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Hashtag> Hashtags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string AuthorUsername { get; set; }
        public int TotalComments { get; set; }
        public int TotalLikes { get; set; }
        public int ReadingTime { get; set; }
        public bool isSavedArticle { get; set; }
    }
}