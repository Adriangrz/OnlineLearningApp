using AutoMapper;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Mapper.Dtos;

namespace OnlineLearningAppApi.Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDto, RequestTokenDto>();
            CreateMap<RefreshTokenDto, RequestTokenDto>();
            //CreateMap<RegistrationDto, User>()
              //  .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}
