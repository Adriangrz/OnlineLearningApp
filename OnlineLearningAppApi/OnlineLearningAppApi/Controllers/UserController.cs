using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("api/Team/{teamId}/[controller]")]
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll([FromRoute] Guid teamId)
        {
            var usersDtos = await _userService.GetAllTeamMembersAsync(teamId);

            return Ok(usersDtos);
        }

        [Route("api/Team/{teamId}/[controller]")]
        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromRoute] Guid teamId, [FromBody] AddUserToTeamDto dto)
        {
            var userDto = await _userService.AddUserToTeamAsync(teamId, dto);

            return Ok(userDto);
        }

        [Route("api/Team/{teamId}/[controller]/{userId}")]
        [HttpDelete]
        public async Task<ActionResult<UserDto>> Delete([FromRoute] Guid teamId, [FromRoute] string userId)
        {
            await _userService.DeleteUserFromTeamAsync(teamId, userId);

            return Ok();
        }

        [Route("api/[controller]/{id}")]
        [HttpGet]
        public async Task<ActionResult<UserDetailsDto>> Get([FromRoute] string id)
        {
            var userDetailsDto = await _userService.GetByIdAsync(id);

            return Ok(userDetailsDto);
        }
    }
}
