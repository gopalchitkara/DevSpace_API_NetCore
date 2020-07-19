using System.Collections.Generic;
using DevSpace_API.Models;

namespace DevSpace_API.Data.Hashtags
{
    public interface IHashtagRepo
    {
        bool SaveChanges();

        IEnumerable<HashtagForWidget> GetHashtagsForWidgets();

        IEnumerable<string> GetHashtagSuggestions(string searchString);
    }
}