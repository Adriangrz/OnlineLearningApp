using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Models.ApiModels;
using OnlineLearningAppApi.Repositories;
using OnlineLearningAppApi.Services.Communication;
using OnlineLearningAppApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly UnitOfWork _unitOfWork;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, UnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<User>> Registration(User userData, string password)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(userData.Email) is not null)
                    return new BaseResponse<User>(false, "Użytkownik już istnieje.");

                var result = await _userManager.CreateAsync(userData, password);
                if (!result.Succeeded)
                    return new BaseResponse<User>(false, "Nie udało się utworzyć użytkownika.");

                await _userManager.AddToRoleAsync(userData, AppRoles.User);

                return new BaseResponse<User>(true, string.Empty, false, userData);
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>(false, "Wystąpił błąd podczas przetwarzania uwierzytelniania", true);
            }
        }

        public async Task<BaseResponse<ResponseTokenData>> AuthenticationAsync(RequestTokenData loginCredentials)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginCredentials.Email);

                if (user is null)
                    return new BaseResponse<ResponseTokenData>(false, "Nieprawidłowe dane logowania.");

                if (await _userManager.IsLockedOutAsync(user))
                    return new BaseResponse<ResponseTokenData>(false, "Użytkownik jest zablokowany.");

                if (!await _userManager.CheckPasswordAsync(user, loginCredentials.Password))
                {
                    await _userManager.AccessFailedAsync(user);
                    return new BaseResponse<ResponseTokenData>(false, "Nieprawidłowe dane logowania.");
                }

                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles is null)
                    return new BaseResponse<ResponseTokenData>(false, "Użytkownik nie ma przypisanej roli.");

                await _userManager.ResetAccessFailedCountAsync(user);

                var rt = CreateRefreshToken(loginCredentials.ClientId, user.Id);

                await _unitOfWork.Repository<Token>().Insert(rt);
                _unitOfWork.SaveChanges();

                var t = GenerateJWT(user, userRoles.ToList(), rt.Value);

                return new BaseResponse<ResponseTokenData>(true, string.Empty, false, t);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ResponseTokenData>(false, "Wystąpił błąd podczas przetwarzania uwierzytelniania", true);
            }
        }

        public async Task<BaseResponse<ResponseTokenData>> RefreshToken(RequestTokenData model)
        {
            try
            {
                var rt = await _unitOfWork.Repository<Token>().GetBy(t => t.ClientId == model.ClientId && t.Value == model.RefreshToken);

                if (rt is null)
                    return new BaseResponse<ResponseTokenData>(false, "Brak tokena odświeżania.");

                var user = await _userManager.FindByIdAsync(rt.UserId.ToString());
                if (user is null)
                    return new BaseResponse<ResponseTokenData>(false, "Użytkownik nie istnieje.");

                var rtNew = CreateRefreshToken(rt.ClientId, rt.UserId);

                _unitOfWork.Repository<Token>().Delete(rt);

                await _unitOfWork.Repository<Token>().Insert(rtNew);

                _unitOfWork.SaveChanges();

                var userRoles = await _userManager.GetRolesAsync(user);
                var response = GenerateJWT(user, userRoles.ToList(), rtNew.Value);

                return new BaseResponse<ResponseTokenData>(true, string.Empty, false, response);
            }
            catch (Exception ex)
            {
                return new BaseResponse<ResponseTokenData>(false, "Wystąpił błąd podczas przetwarzania uwierzytelniania", true);
            }
        }

        private Token CreateRefreshToken(string clientId, string userId)
        {
            return new Token()
            {
                ClientId = clientId,
                UserId = userId,
                Type = 0,
                Value = Guid.NewGuid().ToString("N"),
                CreatedDate = DateTime.UtcNow
            };
        }

        public DateTime GetTokenExpirationDate(string token)
        {
            return new JwtSecurityTokenHandler().ReadToken(token).ValidTo;
        }

        public ResponseTokenData GenerateJWT(User userInfo, List<string> userRoles, string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var claims = new List<Claim>()
            {
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                 new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ResponseTokenData()
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = GetTokenExpirationDate(tokenHandler.WriteToken(token)),
                RefreshToken = refreshToken
            };
        }
    }
}
