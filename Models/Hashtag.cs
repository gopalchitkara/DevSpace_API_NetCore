namespace DevSpace_API.Models
{
    public class Hashtag
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long ArticleId { get; set; }
    }
}