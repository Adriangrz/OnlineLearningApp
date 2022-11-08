using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Exceptions;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Infrastructure.Authorization;
using OnlineLearningAppApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Infrastructure.Services
{
    public class QuestionImageService : IQuestionImageService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly ITeamService _teamService;
        public QuestionImageService(IMapper mapper, ApplicationDbContext dbContext, IAuthorizationService authorizationService, IUserContextService userContextService, ITeamService teamService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _teamService = teamService;
        }

        public async Task UploadAsync(Guid questionId, IFormFile file)
        {
            if (file == null || file.Length == 0 || !file.ContentType.StartsWith("image"))
                throw new BadRequestException("Plik jest wymagany i musi to być obraz");


            var question = await _dbContext
                .Questions
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question is null)
                throw new NotFoundException("Quiz nie istnieje");

            var quiz = await _dbContext
                .Quizzes
                .FirstOrDefaultAsync(q => q.Id == question.QuizId);

            if (quiz is null)
                throw new NotFoundException("Quiz nie istnieje");

            var adminId = await _dbContext
                .Teams
                .Where(r => r.Id == quiz.TeamId)
                .Select(t => t.AdminId)
                .FirstOrDefaultAsync();

            if (adminId is null)
                throw new NotFoundException("Zespół nie istnieje");


            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, adminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            question.ImagePaths = new List<string>();


            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                if (memoryStream.Length >= 20097152)
                    throw new BadRequestException("Rozmiar pliku musi być mniejszy niż 20 MB");

                var questionImage = new QuestionImage<User>()
                {
                    Content = memoryStream.ToArray(),
                    ContentType = file.ContentType,
                    QuestionId = questionId

                };
                questionImage = (await _dbContext.QuestionImages.AddAsync(questionImage)).Entity;

                question.ImagePaths.Add($"api/Question/{questionId}/QuestionImage/{questionImage.Id}");

            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
