using AutoMapper;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;

namespace OnlineLearningAppApi.Mapping
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<LoginDto, RequestTokenData>();
            CreateMap<RefreshTokenDto, RequestTokenData>();
            CreateMap<RegistrationDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
