using System.Collections.Generic;
using DevSpace_API.Dtos;
using DevSpace_API.Models;

namespace DevSpace_API.Data.Article
{
    public interface IArticleRepo
    {
        bool SaveChanges();
        IEnumerable<Models.Article> GetAllArticles();
        IEnumerable<Models.Article> GetFeedArticles();
        IEnumerable<Models.Article> GetArticlesByUserId(string userid);
        IEnumerable<Models.Article> GetArticlesByTopic(string topicName);
        Models.Article GetArticleById(long id);
        void CreateArticle(Models.Article article);
        void AddHashtagsForNewArticle(IEnumerable<Hashtag> hashtags);
        void DeleteArticle(Models.Article article);
    }
}