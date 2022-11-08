using OnlineLearningAppApi.Core.Mapper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Interfaces
{
    public interface IQuestionService
    {
        Task<List<QuestionDto>> CreateAsync(List<CreateQuestionDto> dto, Guid quizId);
        Task<PagedResult<QuestionDto>> GetAllAsync(Guid quizId, QuestionQuery query);
    }
}
