using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Extensions;
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
        public async Task<IActionResult> Login([FromBody] LoginResource loginResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _authService.AuthenticationAsync(loginResource);
            if (!result.Success)
                return Unauthorized(result.Message);

            var response = new TokenResponseResource
            {
                Token = result.Resource,
                Expiration = _authService.GetTokenExpirationDate(result.Resource),
            };

            return Ok(response);
        }

    }
}
