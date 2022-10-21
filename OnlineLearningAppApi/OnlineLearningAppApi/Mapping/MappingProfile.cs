using AutoMapper;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;

namespace OnlineLearningAppApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDto, RequestTokenDto>();
            CreateMap<RefreshTokenDto, RequestTokenDto>();
            CreateMap<RegistrationDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<CreateTeamDto, Team>();
            CreateMap<Team, UpdateTeamDto>();
            CreateMap<Team, TeamDto>().ForMember(dest=>dest.Email, opt=>opt.MapFrom(src=>src.Admin.Email));
            CreateMap<TeamImage, TeamImageDto>();
            CreateMap<User, UserDto>();
            CreateMap<User, UserDetailsDto>();
            CreateMap<CreateQuizDto, Quiz>();
            CreateMap<Quiz, QuizDto>();
            CreateMap<CreateQuestionDto, Question>();
            CreateMap<Question, QuestionDto>();
        }
    }
}
