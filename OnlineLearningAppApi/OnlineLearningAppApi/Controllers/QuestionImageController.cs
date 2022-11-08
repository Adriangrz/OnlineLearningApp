using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Core.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/Questions/{questionId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionImageController : ControllerBase
    {
        private readonly IQuestionImageService _questionImageService;
        public QuestionImageController(IQuestionImageService questionImageService)
        {
            _questionImageService = questionImageService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Upload([FromForm] IFormFile file, [FromRoute] Guid questionId)
        {
            await _questionImageService.UploadAsync(questionId, file);

            return Ok();
        }
    }
}
