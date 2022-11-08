using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;

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
        public async Task<ActionResult<ResponseTokenDto>> Login([FromBody] LoginDto dto)
        {
            var responseTokenData = await _authService.AuthenticationAsync(dto);

            return Ok(responseTokenData);
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<ResponseTokenDto>> RefreshToken([FromBody] RefreshTokenDto dto)
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
