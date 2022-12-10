using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Core.Entities;
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
    public class QuizAuthorizationHandler : AuthorizationHandler<ResourceOperationRequirement, Quiz<User>>
    {
        private readonly ApplicationDbContext _dbContext;
        public QuizAuthorizationHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
            Quiz<User> quiz)
        {

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var userQuiz = await _dbContext
                .UserQuizzes
                .FirstOrDefaultAsync(r => r.UserId == userId && r.QuizId == quiz.Id);

            if (userQuiz is null)
                return;

            if (userQuiz.IsDone == false)
                context.Succeed(requirement);
        }
    }
}
