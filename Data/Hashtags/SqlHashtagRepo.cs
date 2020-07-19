using System.Collections.Generic;
using System.Linq;
using DevSpace_API.DataContext;
using DevSpace_API.Models;

namespace DevSpace_API.Data.Hashtags
{
    public class SqlHashtagRepo : IHashtagRepo
    {
        private readonly ApplicationContext _context;

        public SqlHashtagRepo(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<HashtagForWidget> GetHashtagsForWidgets()
        {
            return _context.HashtagsForWidgets.ToList();
        }

        public IEnumerable<string> GetHashtagSuggestions(string searchString)
        {
            return _context.Hashtags.Where(tag => tag.Description.ToLower().Contains(searchString.ToLower()))
                                    .Select(t => t.Description)
                                    .Distinct()
                                    .Take(10)
                                    .ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}