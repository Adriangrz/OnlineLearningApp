using Microsoft.AspNetCore.Authorization;
using OnlineLearningAppApi.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Authorization
{
    public class TeamAuthorizationHandler : AuthorizationHandler<ResourceOperationRequirement, Team>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
            Team team)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read)
            {
                var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
                if (team.Users.Any(u=>u.Id==userId) || team.AdminId == userId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
