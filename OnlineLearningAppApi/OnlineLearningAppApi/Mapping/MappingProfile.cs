using AutoMapper;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;

namespace OnlineLearningAppApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDto, RequestTokenData>();
            CreateMap<RefreshTokenDto, RequestTokenData>();
            CreateMap<RegistrationDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<CreateTeamDto, Team>();
            CreateMap<Team, TeamDto>();
        }
    }
}
