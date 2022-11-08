using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Exceptions;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Authorization;
using OnlineLearningAppApi.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public QuestionService(IMapper mapper, ApplicationDbContext dbContext, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public async Task<List<QuestionDto>> CreateAsync(List<CreateQuestionDto> dto, Guid quizId)
        {
            var quiz = await _dbContext
                .Quizzes
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz is null)
                throw new NotFoundException("Quiz nie istnieje");

            var team = await _dbContext
                .Teams
                .FirstOrDefaultAsync(r => r.Id == quiz.TeamId);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var questions = _mapper.Map<List<Question<User>>>(dto);
            foreach (var item in questions)
            {
                item.Quiz = quiz;
            }
            await _dbContext.Questions.AddRangeAsync(questions);
            await _dbContext.SaveChangesAsync();

            var questionsDtos = _mapper.Map<List<QuestionDto>>(questions);
            return questionsDtos;
        }

        public async Task<PagedResult<QuestionDto>> GetAllAsync(Guid quizId,QuestionQuery query)
        {
            var baseQuery = _dbContext
                .Questions
                .Where(q => q.QuizId == quizId);
                

            var questions = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            var totalItemsCount = await baseQuery.CountAsync();

            var questionsDtos = _mapper.Map<List<QuestionDto>>(questions);

            var result = new PagedResult<QuestionDto>(questionsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }
    }
}
