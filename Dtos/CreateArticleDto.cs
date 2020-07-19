using System.Collections.Generic;
using DevSpace_API.Models;

namespace DevSpace_API.Dtos
{
    public class CreateArticleDto
    {
        public string Title { get; set; }
        public IEnumerable<string> Hashtags { get; set; }
        public string Body { get; set; }
        public string AuthorId { get; set; }
        public string AuthorUsername { get; set; }
    }
}