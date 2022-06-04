using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Models.ApiModels;
using OnlineLearningAppApi.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<BaseResponse<TokenResponseResource>> AuthenticationAsync(TokenRequestResource loginCredentials);
        Task<BaseResponse<TokenResponseResource>> RefreshToken(TokenRequestResource model);
        DateTime GetTokenExpirationDate(string token);
        TokenResponseResource GenerateJWT(User userInfo, List<string> userRoles, string refreshToken);
    }
}
