using System.Collections.Generic;
using System.Linq;
using DevSpace_API.DataContext;
using Microsoft.EntityFrameworkCore;


namespace DevSpace_API.Data.ReadingList
{
    public class SqlReadingListRepo : IReadingListRepo
    {
        private readonly ApplicationContext _context;

        public SqlReadingListRepo(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.Article> GetSavedArticlesByUserId(string userId)
        {
            var savedArticleIds = _context.ReadingLists
                                    .Where(a => a.UserId == userId)
                                    .Select(x => x.ArticleId)
                                    .ToList();

            return _context.Articles
                            .Where(art => savedArticleIds.Contains(art.Id))
                            .Include(x => x.Hashtags)
                            .Include(x => x.Likes)
                            .Include(x => x.Comments)
                            .ToList();
        }

        public void SaveArticleForUserId(string userId, long articleId)
        {
            var savedArticleFromRepo = _context.ReadingLists
                        .FirstOrDefault(a => a.ArticleId == articleId && a.UserId == userId);

            if (savedArticleFromRepo == null)
            {
                _context.ReadingLists.Add(new Models.ReadingList { UserId = userId, ArticleId = articleId });
            }
        }

        public void UnSaveArticleForUserId(string userId, long articleId)
        {
            var savedArticleFromRepo = _context.ReadingLists
                       .FirstOrDefault(a => a.ArticleId == articleId && a.UserId == userId);

            if (savedArticleFromRepo != null)
            {
                _context.ReadingLists.Remove(savedArticleFromRepo);
            }
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}