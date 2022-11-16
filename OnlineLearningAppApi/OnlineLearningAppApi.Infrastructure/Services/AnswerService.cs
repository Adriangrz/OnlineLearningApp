﻿using AutoMapper;
using Core.Interfaces;
using Core.Mapper.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Exceptions;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Authorization;
using OnlineLearningAppApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        public AnswerService(IMapper mapper, ApplicationDbContext dbContext, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }
        public async Task<AnswerDto> CreateAsync(CreateAnswerDto dto, Guid questionId)
        {
            var question = await _dbContext
                .Questions
                .FirstOrDefaultAsync(r => r.Id == questionId);

            if (question is null)
                throw new NotFoundException("Pytanie nie istnieje");

            var answer = _mapper.Map<Answer<User>>(dto);

            answer.QuestionId = questionId;
            answer.UserId = _userContextService.GetUserId;

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, answer,
                new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var userQuiz = await _dbContext
                .UserQuizzes
                .FirstOrDefaultAsync(r => r.UserId == _userContextService.GetUserId && r.QuizId == question.QuizId);

            if (userQuiz is null)
                throw new NotFoundException("Użytkownik nie ma przypisanego testu");

            userQuiz.IsDone = true;

            await _dbContext.Answers.AddAsync(answer);
            await _dbContext.SaveChangesAsync();


            var answerDto = _mapper.Map<AnswerDto>(answer);
            return answerDto;
        }
    }
}