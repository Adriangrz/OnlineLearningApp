using AutoMapper;
using Core.Interfaces;
using Core.Mapper.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Services;

namespace OnlineLearningAppApi.Controllers
{
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        [Route("api/Question/{questionId}/[controller]")]
        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateAnswer([FromBody] CreateAnswerDto dto, [FromRoute] Guid questionId)
        {
            var answer = await _answerService.CreateAsync(dto, questionId);

            return Ok(answer);
        }
        [Route("api/Quiz/{quizId}/User/{userId}/Answer")]
        [HttpGet]
        public async Task<ActionResult<List<AnswerDto>>> GetAll([FromRoute] string userId, [FromRoute] Guid quizId)
        {
            var answers = await _answerService.GetAllAsync(userId, quizId);

            return Ok(answers);
        }
    }
}
