using AutoMapper;
using DevSpace_API.Dtos;
using DevSpace_API.Models;

namespace DevSpace_API.Profiles
{
    public class HashtagsProfile : Profile
    {
        public HashtagsProfile()
        {
            CreateMap<HashtagForWidget, ReadHashtagForWidgetDto>();
        }
    }
}