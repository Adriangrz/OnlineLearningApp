using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper.Dtos
{
    public class AnswerDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string? CodeLanguage { get; set; }
        public string? Code { get; set; }
        public Guid QuestionId { get; set; }
        public QuestionDto Question { get; set; }
        public string UserId { get; set; }
    }
}
