using System.Collections.Generic;

namespace DevSpace_API.Models
{
    public class ReadingList
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long ArticleId { get; set; }
    }
}