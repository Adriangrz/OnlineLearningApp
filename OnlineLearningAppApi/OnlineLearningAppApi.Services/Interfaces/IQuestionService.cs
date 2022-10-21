using OnlineLearningAppApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<List<QuestionDto>> CreateAsync(List<CreateQuestionDto> dto, Guid quizId);
    }
}
