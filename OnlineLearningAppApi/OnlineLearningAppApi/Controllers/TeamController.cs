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
        public async Task<ActionResult<TeamDto>> CreateTeam([FromBody] CreateTeamDto dto)
        {
            var team = await _teamService.CreateAsync(dto);

            return Ok(team);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetById([FromRoute] Guid id)
        {
            var team = await _teamService.GetByIdAsync(id);

            return Ok(team);
        }
        [HttpGet]
        public async Task<ActionResult<List<TeamDto>>> GetAll([FromQuery] TeamQuery teamQuery)
        {
            var result = await _teamService.GetAllAsync(teamQuery);

            return Ok(result);
        }
        [HttpPut("{id}/IsArchived")]
        public async Task<ActionResult<TeamDto>> UpdateTeamIsArchived([FromBody] UpdateTeamIsArchivedDto dto, [FromRoute] Guid id)
        {

            var team = await _teamService.UpdateTeamIsArchivedAsync(id, dto);

            return Ok(team);
        }
        [HttpPut("{id}/Name")]
        public async Task<ActionResult<TeamDto>> UpdateTeamName([FromBody] UpdateTeamNameDto dto, [FromRoute] Guid id)
        {

            var team = await _teamService.UpdateTeamNameAsync(id, dto);

            return Ok(team);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TeamDto>> Update([FromBody] UpdateTeamDto dto, [FromRoute] Guid id)
        {

            var team = await _teamService.UpdateAsync(id, dto);

            return Ok(team);
        }
    }
}
