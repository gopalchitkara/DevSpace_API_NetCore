using System;
using System.Linq;
using DevSpace_API.DataContext;
using DevSpace_API.Models;

namespace DevSpace_API.Data.Likes
{
    public class SqlLikeRepo : ILikeRepo
    {
        private readonly ApplicationContext _context;

        public SqlLikeRepo(ApplicationContext context)
        {
            _context = context;
        }
        public Like GetLikeByUserAndArticleId(string userId, long articleId)
        {
            return _context.Likes.FirstOrDefault(x => x.ArticleId == articleId && x.UserId == userId);
        }

        public void AddLike(Like like)
        {
            _context.Likes.Add(like);
        }

        public void RemoveLike(Like like)
        {
            _context.Likes.Remove(like);
        }

        public bool SaveChanes()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}