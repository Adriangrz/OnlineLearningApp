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
        Task<BaseResponse<ResponseTokenData>> AuthenticationAsync(RequestTokenData loginCredentials);
        Task<BaseResponse<ResponseTokenData>> RefreshToken(RequestTokenData model);
        DateTime GetTokenExpirationDate(string token);
        ResponseTokenData GenerateJWT(User userInfo, List<string> userRoles, string refreshToken);
        Task<BaseResponse<User>> Registration(User userData, string password);
    }
}
