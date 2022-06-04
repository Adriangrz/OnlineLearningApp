using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Models.ApiModels;
using OnlineLearningAppApi.Services;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthenticationController(IAuthService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        // POST api/<AuthenticateController>/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginResource loginResource)
        {
            var tokenRequestData = _mapper.Map<LoginResource, TokenRequestData>(loginResource);
            var result = await _authService.AuthenticationAsync(tokenRequestData);
            if (!result.Success && result.IsException)
                return StatusCode(500,result.Message);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result.Resource);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenResource refreshTokenResource)
        {
            var tokenRequestData = _mapper.Map<RefreshTokenResource, TokenRequestData>(refreshTokenResource);
            var result = await _authService.RefreshToken(tokenRequestData);
            if (!result.Success && result.IsException)
                return StatusCode(500, result.Message);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result.Resource);
        }

    }
}
