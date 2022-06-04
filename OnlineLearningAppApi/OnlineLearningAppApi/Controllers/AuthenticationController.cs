using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/<AuthenticateController>/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] TokenRequestResource loginResource)
        {
            var result = await _authService.AuthenticationAsync(loginResource);
            if (!result.Success && result.IsException)
                return StatusCode(500,result.Message);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result.Resource);
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestResource refreshResource)
        {
            var result = await _authService.RefreshToken(refreshResource);
            if (!result.Success && result.IsException)
                return StatusCode(500, result.Message);

            if (!result.Success)
                return Unauthorized(result.Message);

            return Ok(result.Resource);
        }

    }
}
