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
        Task<BaseResponse<string>> AuthenticationAsync(LoginResource loginCredentials);
        DateTime GetTokenExpirationDate(string token);
        string GenerateJWT(User userInfo, List<string> userRoles);
    }
}
