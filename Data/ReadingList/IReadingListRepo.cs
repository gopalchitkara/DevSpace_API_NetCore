using System.Collections.Generic;

namespace DevSpace_API.Data.ReadingList
{
    public interface IReadingListRepo
    {
        //GetSavedArticlesByUserId
        public IEnumerable<Models.Article> GetSavedArticlesByUserId(string userId);

        //SaveArticleForUserId
        public void SaveArticleForUserId(string userId, long articleId);

        //UnSaveArticleForUserId
        public void UnSaveArticleForUserId(string userId, long articleId);

        public bool SaveChanges();

    }
}