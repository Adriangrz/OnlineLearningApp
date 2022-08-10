using AutoMapper;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Models.ApiModels;

namespace OnlineLearningAppApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<LoginResource, RequestTokenData>();
            CreateMap<RefreshTokenResource, RequestTokenData>();
            CreateMap<RegistrationResource, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
