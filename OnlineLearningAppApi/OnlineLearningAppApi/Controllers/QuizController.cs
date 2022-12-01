using AutoMapper;
using Core.Mapper.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Services;
using OnlineLearningAppApi.Services;

namespace OnlineLearningAppApi.Controllers
{
    [ApiController]
    [Authorize]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [Route("api/Team/{teamId}/[controller]")]
        [HttpPost]
        public async Task<ActionResult<QuizDto>> CreateQuiz([FromBody] CreateQuizDto dto, [FromRoute] Guid teamId)
        {
            var quiz = await _quizService.CreateAsync(dto, teamId);

            return Ok(quiz);
        }

        [Route("api/Team/{teamId}/[controller]")]
        [HttpGet]
        public async Task<ActionResult<List<QuizDto>>> GetAll([FromRoute] Guid teamId)
        {
            var result = await _quizService.GetAllAsync(teamId);

            return Ok(result);
        }

        [Route("api/Team/{teamId}/[controller]/{quizId}")]
        [HttpGet]
        public async Task<ActionResult<QuizDetailsDto>> GetById([FromRoute] Guid teamId, [FromRoute] Guid quizId)
        {
            var quiz = await _quizService.GetByIdAsync(teamId,quizId);

            return Ok(quiz);
        }

        [Route("api/[controller]/{quizId}/User/{userId}/Grade")]
        [HttpPost]
        public async Task<ActionResult> RateCompletedQuiz([FromRoute] Guid quizId, [FromRoute] string userId, [FromBody] GradeDto gradeDto)
        {
            await _quizService.RateCompletedQuiz(quizId,userId, gradeDto);

            return Ok();
        }
    }
}
