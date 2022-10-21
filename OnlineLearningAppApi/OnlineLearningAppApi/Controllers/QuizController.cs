using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/Team/{teamId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz([FromBody] CreateQuizDto dto, [FromRoute] Guid teamId)
        {
            var quiz = await _quizService.CreateAsync(dto, teamId);

            return Ok(quiz);
        }

        [HttpGet]
        public async Task<ActionResult<List<QuizDto>>> GetAll([FromRoute] Guid teamId)
        {
            var result = await _quizService.GetAllAsync(teamId);

            return Ok(result);
        }
    }
}
