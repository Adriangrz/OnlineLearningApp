using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Services;

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

        [HttpGet("{quizId}")]
        public async Task<ActionResult<QuizDetailsDto>> GetById([FromRoute] Guid teamId, [FromRoute] Guid quizId)
        {
            var quiz = await _quizService.GetByIdAsync(teamId,quizId);

            return Ok(quiz);
        }
    }
}
