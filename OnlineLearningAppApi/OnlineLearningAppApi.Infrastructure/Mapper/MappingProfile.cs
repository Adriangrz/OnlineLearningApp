using AutoMapper;
using Core.Mapper.Dtos;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Persistence;

namespace OnlineLearningAppApi.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegistrationDto, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<User, UserDto>();
            CreateMap<User, UserDetailsDto>();
            CreateMap<CreateTeamDto, Team<User>>();
            CreateMap<Team<User>, UpdateTeamDto>();
            CreateMap<Team<User>, TeamDto>().ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Admin.Email));
            CreateMap<TeamImage<User>, TeamImageDto>();
            CreateMap<CreateQuizDto, Quiz<User>>();
            CreateMap<Quiz<User>, QuizDto>();
            CreateMap<Quiz<User>, QuizDetailsDto>();
            CreateMap<CreateQuestionDto, Question<User>>();
            CreateMap<Question<User>, QuestionDto>();
            CreateMap<CreateAnswerDto, Answer<User>>();
            CreateMap<Answer<User>, AnswerDto>();
            CreateMap<UserQuiz<User>, QuizUserDto>();
        }
    }
}
