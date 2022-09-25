using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/Team/{teamId}/[controller]")]
    [ApiController]
    public class TeamImageController : ControllerBase
    {
        private readonly ITeamImageService _teamImageService;
        public TeamImageController(ITeamImageService teamImageService)
        {
            _teamImageService = teamImageService;
        }

        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "teamId" })]
        public async Task<ActionResult> GetImage([FromRoute] Guid teamId)
        {
            var image = await _teamImageService.GetImageAsync(teamId);

            return File(image.Content, image.ContentType);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Upload([FromForm] IFormFile file, [FromRoute] Guid teamId)
        {
            await _teamImageService.UploadAsync(teamId, file);

            return Ok();
        }
    }
}
