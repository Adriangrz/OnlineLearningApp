using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Exceptions;
using OnlineLearningAppApi.Infrastructure.Authorization;
using OnlineLearningAppApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authorization
{
    public class AnswerAuthorizationHandler : AuthorizationHandler<ResourceOperationRequirement, Answer<User>>
    {
        private readonly ApplicationDbContext _dbContext;
        public AnswerAuthorizationHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
            Answer<User> answer)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == answer.QuestionId);

            if (question is null)
                return;

            var quiz = await _dbContext
                .Quizzes
                .FirstOrDefaultAsync(q => q.Id == question.QuizId);

            if (quiz is null)
                return;

            var team = await _dbContext
                .Teams
                .Include(t => t.Users)
                .FirstOrDefaultAsync(r => r.Id == quiz.TeamId);

            if (team is null)
                return;

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var userQuiz = await _dbContext
                .UserQuizzes
                .FirstOrDefaultAsync(r => r.UserId == userId && r.QuizId == question.QuizId);

            if (userQuiz is null)
                return;

            if (userQuiz.IsDone == false && (team.Users.Any(u => u.Id == userId) || team.AdminId == userId))
                context.Succeed(requirement);

        }
    }
}
