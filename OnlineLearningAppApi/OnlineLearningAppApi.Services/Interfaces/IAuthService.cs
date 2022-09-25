using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseTokenDto> AuthenticationAsync(LoginDto loginDto);
        Task<ResponseTokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        DateTime GetTokenExpirationDate(string token);
        ResponseTokenDto GenerateJWT(User userInfo, List<string> userRoles, string refreshToken);
        Task RegistrationAsync(RegistrationDto registrationDto);
    }
}
