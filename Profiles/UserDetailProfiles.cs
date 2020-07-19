using AutoMapper;
using DevSpace_API.Dtos;
using DevSpace_API.Models;

namespace DevSpace_API.Profiles
{
    public class UserDetailProfiles : Profile
    {
        public UserDetailProfiles()
        {
            CreateMap<UpdateUserDetailDto, UserDetail>();
        }
    }
}