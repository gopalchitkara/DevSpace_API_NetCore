using AutoMapper;
using DevSpace_API.Dtos;
using DevSpace_API.Models;
using static DevSpace_API.Constants.Constants;

namespace DevSpace_API.Profiles
{
    public class ArticleProfiles : Profile
    {
        public ArticleProfiles()
        {
            CreateMap<Article, ReadArticleDto>();
            CreateMap<CreateArticleDto, Article>();
            CreateMap<Article, FeedArticleDto>()
            .ForMember(dest => dest.TotalLikes, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.TotalComments, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.ReadingTime, opt => opt.MapFrom(src => (src.Body.Length / (averageCharPerWord * readingSpeed))));
        }
    }
}