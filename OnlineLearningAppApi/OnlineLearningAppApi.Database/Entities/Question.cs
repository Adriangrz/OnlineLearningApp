using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database.Entities
{
    public enum AnswerType
    {
        Code,
        Text,
        MultipleChoiceAnswer
    }
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public AnswerType AnswerType { get; set; }
        public List<string>? MultipleChoiceOptions { get; set; }
        public string? CodeLanguage { get; set; }
        public string? Code { get; set; }
        public List<string>? ImagePaths { get; set; }
        public ICollection<QuestionImage> QuestionImages { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
