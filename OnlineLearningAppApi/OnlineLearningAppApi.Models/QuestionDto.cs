using OnlineLearningAppApi.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Models
{
    public class QuestionDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public AnswerType AnswerType { get; set; }
        public string[] MultipleChoiceOptions { get; set; }
        public string? CodeLanguage { get; set; }
        public string? Code { get; set; }
        public bool HasImages { get; set; }
        public Guid QuizId { get; set; }
    }
}
