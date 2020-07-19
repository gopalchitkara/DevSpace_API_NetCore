using System.Collections.Generic;
using System.Linq;
using DevSpace_API.DataContext;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using Microsoft.EntityFrameworkCore;

namespace DevSpace_API.Data.Article
{
    public class SqlArticleRepo : IArticleRepo
    {
        private readonly ApplicationContext _context;

        public SqlArticleRepo(ApplicationContext context)
        {
            _context = context;
        }

        public void CreateArticle(Models.Article article)
        {
            _context.Articles.Add(article);
        }

        public void AddHashtagsForNewArticle(IEnumerable<Hashtag> hashtags)
        {
            _context.Hashtags.AddRange(hashtags);
        }

        public IEnumerable<Models.Article> GetAllArticles()
        {
            return _context.Articles.Include(x => x.Hashtags)
                                    .Include(x => x.Likes)
                                    .Include(x => x.Comments)
                                    .ToList();
        }
        public IEnumerable<Models.Article> GetFeedArticles()
        {
            return _context.Articles.Include(x => x.Hashtags)
                                    .Include(x => x.Likes)
                                    .Include(x => x.Comments)
                                    .ToList();
        }

        public Models.Article GetArticleById(long id)
        {
            return _context.Articles.Where(article => article.Id == id)
                                    .Include(x => x.Hashtags)
                                    .Include(x => x.Likes)
                                    .Include(x => x.Comments)
                                    .FirstOrDefault();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public IEnumerable<Models.Article> GetArticlesByUserId(string userid)
        {
            return _context.Articles.Where(article => article.AuthorId == userid)
                                    .Include(x => x.Hashtags)
                                    .Include(x => x.Likes)
                                    .Include(x => x.Comments)
                                    .ToList();
        }

        public IEnumerable<Models.Article> GetArticlesByTopic(string topicName)
        {
            var query = _context.Hashtags.Where(x => x.Description.Equals(topicName))
                                        .Select(y => y.ArticleId);

            return _context.Articles.Where(x => query.Contains(x.Id))
                                    .Include(x => x.Hashtags)
                                    .Include(x => x.Likes)
                                    .Include(x => x.Comments)
                                    .ToList();
        }

        public void DeleteArticle(Models.Article article)
        {
            _context.Articles.Remove(article);
        }
    }
}