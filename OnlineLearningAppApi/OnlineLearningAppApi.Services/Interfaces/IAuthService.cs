using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseTokenData> AuthenticationAsync(LoginDto loginDto);
        Task<ResponseTokenData> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        DateTime GetTokenExpirationDate(string token);
        ResponseTokenData GenerateJWT(User userInfo, List<string> userRoles, string refreshToken);
        Task RegistrationAsync(RegistrationDto registrationDto);
    }
}
