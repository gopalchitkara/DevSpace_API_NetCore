using DevSpace_API.Models;

namespace DevSpace_API.Data.Userdetails
{
    public interface IUserDetailRepo
    {
        bool SaveChanges();
        UserDetail GetUserDetailByUsername(string username);
        UserDetail GetUserDetailByUserid(string userId);
        void UpdateUserDetail(UserDetail userDetail);
    }
}