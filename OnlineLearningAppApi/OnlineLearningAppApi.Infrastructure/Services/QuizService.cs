using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OnlineLearningAppApi.Core.Exceptions;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Authorization;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Infrastructure.Persistence;
using OnlineLearningAppApi.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OnlineLearningAppApi.Infrastructure.Services
{
    public class QuizService : IQuizService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public QuizService(IMapper mapper, ApplicationDbContext dbContext, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }

        public async Task<QuizDto> CreateAsync(CreateQuizDto dto, Guid teamId)
        {
            var team = await _dbContext
                .Teams
                .Include(t => t.Users)
                .FirstOrDefaultAsync(r => r.Id == teamId);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var quiz = _mapper.Map<Quiz<User>>(dto);
            quiz.Team = team;
            quiz.Users = new List<User>();
            quiz.CreatedDate = DateTime.Now;
            foreach (var user in team.Users)
            {
                quiz.Users.Add(user);
            }
            await _dbContext.Quizzes.AddAsync(quiz);
            await _dbContext.SaveChangesAsync();

            var quizDto = _mapper.Map<QuizDto>(quiz);
            return quizDto;
        }

        public async Task<QuizDetailsDto> GetByIdAsync(Guid teamId, Guid quizId)
        {
            var team = await _dbContext
                .Teams
                .Include(t => t.Users)
                .FirstOrDefaultAsync(r => r.Id == teamId);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var quiz = await _dbContext.Quizzes.FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz is null)
                throw new NotFoundException("Test nie istnieje");

            var quizDetailsDto = _mapper.Map<QuizDetailsDto>(quiz);

            var userQuiz = await _dbContext.UserQuizzes.FirstOrDefaultAsync(uq => uq.UserId == _userContextService.GetUserId && uq.QuizId == quizId);
            
            if (userQuiz is not null)
                quizDetailsDto.IsDone = userQuiz.IsDone;
            
            return quizDetailsDto;
        }

        public async Task<List<QuizDto>> GetAllAsync(Guid teamId)
        {
            var team = await _dbContext
                .Teams
                .Include(t => t.Users)
                .FirstOrDefaultAsync(r => r.Id == teamId);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var quizzes = await _dbContext.Quizzes.Where(q => q.TeamId == teamId).ToListAsync();

            var quzzesDtos = _mapper.Map<List<QuizDto>>(quizzes);

            return quzzesDtos;
        }

    }
}
