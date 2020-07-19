using System;
using AutoMapper;
using DevSpace_API.Dtos;
using DevSpace_API.Models;

namespace DevSpace_API.Profiles
{
    public class LikeProfile : Profile
    {
        public LikeProfile()
        {
            CreateMap<CreateLikeDto, Like>()
                .ForMember(dest => dest.ActionCreatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}