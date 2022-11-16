using AutoMapper;
using Core.Interfaces;
using Core.Mapper.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;

namespace OnlineLearningAppApi.Controllers
{
    [Route("api/Question/{questionId}/[controller]")]
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }
        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateAnswer([FromBody] CreateAnswerDto dto, [FromRoute] Guid questionId)
        {
            var answer = await _answerService.CreateAsync(dto, questionId);

            return Ok(answer);
        }
    }
}
