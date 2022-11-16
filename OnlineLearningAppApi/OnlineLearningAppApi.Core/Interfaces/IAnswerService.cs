using Core.Mapper.Dtos;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAnswerService
    {
        Task<AnswerDto> CreateAsync(CreateAnswerDto dto, Guid questionId);
    }
}
