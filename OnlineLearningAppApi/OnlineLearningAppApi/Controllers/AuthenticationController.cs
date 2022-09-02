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

        // POST api/<AuthenticateController>/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseTokenData>> Login([FromBody] LoginDto loginDto)
        {
            var responseTokenData = await _authService.AuthenticationAsync(loginDto);

            return Ok(responseTokenData);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseTokenData>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var responseTokenData = await _authService.RefreshTokenAsync(refreshTokenDto);

            return Ok(responseTokenData);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            await _authService.RegistrationAsync(registrationDto);

            return Ok();
        }
    }
}
