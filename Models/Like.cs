using System;

namespace DevSpace_API.Models
{
    public class Like
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public DateTime ActionCreatedAt { get; set; }
        public long ArticleId { get; set; }
    }
}