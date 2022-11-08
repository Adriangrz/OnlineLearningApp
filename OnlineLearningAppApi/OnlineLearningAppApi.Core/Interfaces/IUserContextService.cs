using System.Security.Claims;

namespace OnlineLearningAppApi.Core.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        string? GetUserId { get; }
    }
}
