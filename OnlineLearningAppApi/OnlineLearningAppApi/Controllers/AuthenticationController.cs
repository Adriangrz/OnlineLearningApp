using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseTokenData>> Login([FromBody] LoginDto dto)
        {
            var responseTokenData = await _authService.AuthenticationAsync(dto);

            return Ok(responseTokenData);
        }

        [HttpPost("RefreshToken")]
        [Authorize]
        public async Task<ActionResult<ResponseTokenData>> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var responseTokenData = await _authService.RefreshTokenAsync(dto);

            return Ok(responseTokenData);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegistrationDto dto)
        {
            await _authService.RegistrationAsync(dto);

            return Ok();
        }
    }
}
