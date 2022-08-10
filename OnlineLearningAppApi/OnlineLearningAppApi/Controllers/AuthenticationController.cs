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
            var requestTokenData = _mapper.Map<LoginResource, RequestTokenData>(loginResource);
            var result = await _authService.AuthenticationAsync(requestTokenData);
            if (!result.Success && result.IsException)
                return StatusCode(500, result.Message);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result.Resource);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenResource refreshTokenResource)
        {
            var requestTokenData = _mapper.Map<RefreshTokenResource, RequestTokenData>(refreshTokenResource);
            var result = await _authService.RefreshToken(requestTokenData);
            if (!result.Success && result.IsException)
                return StatusCode(500, result.Message);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result.Resource);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegistrationResource registrationResource)
        {
            var user = _mapper.Map<RegistrationResource, User>(registrationResource);
            var result = await _authService.Registration(user, registrationResource.Password);
            if (!result.Success && result.IsException)
                return StatusCode(500, result.Message);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
