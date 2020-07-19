using DevSpace_API.Models;

namespace DevSpace_API.Data.Likes
{
    public interface ILikeRepo
    {
        bool SaveChanes();
        Like GetLikeByUserAndArticleId(string userId, long articleId);
        void AddLike(Like like);
        void RemoveLike(Like like);
    }
}