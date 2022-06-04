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
        Task<BaseResponse<TokenResponseData>> AuthenticationAsync(TokenRequestData loginCredentials);
        Task<BaseResponse<TokenResponseData>> RefreshToken(TokenRequestData model);
        DateTime GetTokenExpirationDate(string token);
        TokenResponseData GenerateJWT(User userInfo, List<string> userRoles, string refreshToken);
    }
}
