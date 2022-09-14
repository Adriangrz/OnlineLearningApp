using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService, IMapper mapper)
        {
            _teamService = teamService;
        }
    }
}
