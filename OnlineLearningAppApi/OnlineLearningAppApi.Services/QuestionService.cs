using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Database;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services.Authorization;
using OnlineLearningAppApi.Services.Exceptions;
using OnlineLearningAppApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services
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

            var questions = _mapper.Map<List<Question>>(dto);
            foreach (var item in questions)
            {
                item.Quiz = quiz;
            }
            await _dbContext.Questions.AddRangeAsync(questions);
            await _dbContext.SaveChangesAsync();

            var questionsDtos = _mapper.Map<List<QuestionDto>>(questions);
            return questionsDtos;
        }
    }
}
