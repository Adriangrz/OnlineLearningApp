using System.Security.Claims;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        string? GetUserId { get; }
    }
}
