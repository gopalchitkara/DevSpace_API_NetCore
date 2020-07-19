using System;

namespace DevSpace_API.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Body { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UserId { get; set; }
        public long ArticleId { get; set; }
    }
}