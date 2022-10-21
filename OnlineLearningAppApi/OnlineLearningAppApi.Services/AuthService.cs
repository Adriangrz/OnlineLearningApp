using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineLearningAppApi.Database;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services.Exceptions;
using OnlineLearningAppApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineLearningAppApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IMapper mapper, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task RegistrationAsync(RegistrationDto registrationDto)
        {
            var userData = _mapper.Map<User>(registrationDto);

            if (await _userManager.FindByEmailAsync(userData.Email) is not null)
                throw new BadRequestException("Użytkownik już istnieje");

            var result = await _userManager.CreateAsync(userData, registrationDto.Password);
            if (!result.Succeeded)
                throw new Exception();

            result = await _userManager.AddToRoleAsync(userData, "User");
            if (!result.Succeeded)
                throw new Exception();
        }

        public async Task<ResponseTokenDto> AuthenticationAsync(LoginDto loginDto)
        {
            var loginCredentials = _mapper.Map<RequestTokenDto>(loginDto);

            var user = await _userManager.FindByEmailAsync(loginCredentials.Email);

            if (user is null)
                throw new UnauthorizedException("Nieprawidłowa nazwa użytkownika lub hasło");

            if (await _userManager.IsLockedOutAsync(user))
                throw new UnauthorizedException("Użytkownik jest zablokowany");

            if (!await _userManager.CheckPasswordAsync(user, loginCredentials.Password))
            {
                await _userManager.AccessFailedAsync(user);
                throw new UnauthorizedException("Nieprawidłowa nazwa użytkownika lub hasło");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles is null)
                throw new UnauthorizedException("Użytkownik nie ma przypisanej roli");

            await _userManager.ResetAccessFailedCountAsync(user);

            var rt = CreateRefreshToken(loginCredentials.ClientId, user.Id);

            await _dbContext.Tokens.AddAsync(rt);
            await _dbContext.SaveChangesAsync();

            var t = GenerateJWT(user, userRoles.ToList(), rt.Value);

            return t;

        }

        public async Task<ResponseTokenDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var model = _mapper.Map<RequestTokenDto>(refreshTokenDto);

            var rt = await _dbContext.Tokens.FirstOrDefaultAsync(t => t.ClientId == model.ClientId && t.Value == model.RefreshToken);

            if (rt is null)
                throw new UnauthorizedException("Brak tokena odświeżania");

            var user = await _userManager.FindByIdAsync(rt.UserId.ToString());
            if (user is null)
                throw new UnauthorizedException("Użytkownik nie istnieje");

            var rtNew = CreateRefreshToken(rt.ClientId, rt.UserId);

            _dbContext.Tokens.Remove(rt);

            await _dbContext.Tokens.AddAsync(rtNew);

            await _dbContext.SaveChangesAsync();

            var userRoles = await _userManager.GetRolesAsync(user);
            var response = GenerateJWT(user, userRoles.ToList(), rtNew.Value);

            return response;

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

        public ResponseTokenDto GenerateJWT(User userInfo, List<string> userRoles, string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var claims = new List<Claim>()
            {
                 new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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

            return new ResponseTokenDto()
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = GetTokenExpirationDate(tokenHandler.WriteToken(token)),
                RefreshToken = refreshToken
            };
        }
    }
}
