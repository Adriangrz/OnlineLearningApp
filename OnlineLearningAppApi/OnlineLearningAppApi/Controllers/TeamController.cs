using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
        }
        [HttpPost]
        public async Task<ActionResult> CreateTeam([FromBody] CreateTeamDto dto)
        {
            var team = await _teamService.CreateAsync(dto);

            return Ok(team);
        }
        [HttpGet]
        public async Task<ActionResult<PagedResult<TeamDto>>> GetAll([FromQuery] TeamQuery query)
        {
            var result = await _teamService.GetAllAsync(query);

            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TeamDto>> UpdateAsync([FromBody] UpdateTeamDto dto, [FromRoute] Guid id)
        {

            var team = await _teamService.UpdateAsync(id, dto);

            return Ok(team);
        }
    }
}
