using AutoMapper;
using DevSpace_API.Dtos;
using DevSpace_API.Models;

namespace DevSpace_API.Profiles
{
    public class DraftsProfile : Profile
    {
        public DraftsProfile()
        {
            //src -> dest
            CreateMap<Draft, ReadDraftDto>();
        }
    }
}