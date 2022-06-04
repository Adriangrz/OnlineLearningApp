using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Models.ApiModels;
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

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<BaseResponse<string>> AuthenticationAsync(TokenRequestResource loginCredentials)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginCredentials.Email);

                if (user is null)
                    return new BaseResponse<string>(false, "Nieprawidłowe dane logowania.");

                if (await _userManager.IsLockedOutAsync(user))
                    return new BaseResponse<string>(false, "Użytkownik jest zablokowany.");

                if (!await _userManager.CheckPasswordAsync(user, loginCredentials.Password))
                {
                    await _userManager.AccessFailedAsync(user);
                    return new BaseResponse<string>(false, "Nieprawidłowe dane logowania.");
                }

                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles is null)
                    return new BaseResponse<string>(false, "Użytkownik nie ma przypisanej roli.");

                string token = GenerateJWT(user, userRoles.ToList());

                return new BaseResponse<string>(true, string.Empty, false, token);
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>(false, "Wystąpił błąd podczas przetwarzania uwierzytelniania", true);
            }
        }

        //public async Task<BaseResponse<string>> RefreshToken(TokenRequestViewModel model)
        //{
        //    try
        //    {
        //        // Sprawdź, czy otrzymany token odświeżania istnieje dla danego ClientId
        //        var rt = DbContext.Tokens
        //        .FirstOrDefault(t =>
        //        t.ClientId == model.client_id
        //        && t.Value == model.refresh_token);
        //        if (rt == null)
        //        {
        //            // Token nie istnieje lub jest niepoprawny (albo przekazano złe ClientId)
        //            return new UnauthorizedResult();
        //        }
        //        // Sprawdź, czy istnieje użytkownik o UserId z tokena odświeżania
        //        var user = await UserManager.FindByIdAsync(rt.UserId);
        //        if (user == null)
        //        {
        //            // Użytkownika nie odnaleziono lub UserId jest nieprawidłowe
        //            return new UnauthorizedResult();
        //        }
        //        // Wygeneruj nowy token odświeżania
        //        var rtNew = CreateRefreshToken(rt.ClientId, rt.UserId);
        //        // Unieważnij stary token odświeżania (poprzez jego usunięcie)
        //        DbContext.Tokens.Remove(rt);
        //        // Dodaj nowy token odświeżania
        //        DbContext.Tokens.Add(rtNew);
        //        // Zapisz zmiany w bazie danych
        //        DbContext.SaveChanges();
        //        // Utwórz nowy token dostępowy
        //        var response = CreateAccessToken(rtNew.UserId, rtNew.Value);
        //        // …i wyślij go do klienta
        //        return Json(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new UnauthorizedResult();
        //    }
        //}

        public DateTime GetTokenExpirationDate(string token)
        {
            return new JwtSecurityTokenHandler().ReadToken(token).ValidTo;
        }

        public string GenerateJWT(User userInfo, List<string> userRoles)
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

            return tokenHandler.WriteToken(token);
        }
    }
}
