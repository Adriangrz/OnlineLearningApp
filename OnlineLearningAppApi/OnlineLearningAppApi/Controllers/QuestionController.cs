using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/Quiz/{quizId}/[controller]")]
    [ApiController]
    [Authorize]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpPost]
        public async Task<ActionResult<List<QuestionDto>>> CreateQuestions([FromBody] List<CreateQuestionDto> dtos, [FromRoute] Guid quizId)
        {
            var questions = await _questionService.CreateAsync(dtos, quizId);

            return Ok(questions);
        }
        [HttpGet]
        public async Task<ActionResult<PagedResult<QuestionDto>>> GetAll([FromQuery] QuestionQuery query, [FromRoute] Guid quizId)
        {
            var questionsDtos = await _questionService.GetAllAsync(quizId, query);

            return Ok(questionsDtos);
        }
    }
}
