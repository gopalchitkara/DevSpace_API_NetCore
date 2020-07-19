using System.Linq;
using AutoMapper;
using DevSpace_API.DataContext;
using DevSpace_API.Models;

namespace DevSpace_API.Data.Userdetails
{
    public class SqlUserDetailRepo : IUserDetailRepo
    {
        private readonly ApplicationContext _context;

        public SqlUserDetailRepo(ApplicationContext context)
        {
            _context = context;
        }
        public UserDetail GetUserDetailByUsername(string username)
        {
            return _context.UserDetails.FirstOrDefault(u => u.Username == username);
        }

        public UserDetail GetUserDetailByUserid(string userId)
        {
            return _context.UserDetails.FirstOrDefault(u => u.UserId == userId);
        }

        public void UpdateUserDetail(UserDetail userDetail)
        {
            //nothing
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}