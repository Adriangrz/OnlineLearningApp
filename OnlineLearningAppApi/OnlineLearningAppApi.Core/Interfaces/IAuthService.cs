
using OnlineLearningAppApi.Core.Mapper.Dtos;

namespace OnlineLearningAppApi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseTokenDto> AuthenticationAsync(LoginDto loginDto);
        Task<ResponseTokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        DateTime GetTokenExpirationDate(string token);
        Task<ResponseTokenDto> GenerateJWTAsync(string userId, List<string> userRoles, string refreshToken);
        Task RegistrationAsync(RegistrationDto registrationDto);
    }
}
