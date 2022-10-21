using OnlineLearningAppApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface IQuizService
    {
        Task<QuizDto> CreateAsync(CreateQuizDto dto, Guid teamId);
        Task<List<QuizDto>> GetAllAsync(Guid teamId);
    }
}
