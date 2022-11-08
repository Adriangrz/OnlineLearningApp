using Core.Interfaces;
using OnlineLearningAppApi.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Entities
{
    public class Question<TUser> where TUser : IUser
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public AnswerType AnswerType { get; set; }
        public List<string>? MultipleChoiceOptions { get; set; }
        public string? CodeLanguage { get; set; }
        public string? Code { get; set; }
        public List<string>? ImagePaths { get; set; }
        public ICollection<QuestionImage<TUser>> QuestionImages { get; set; }
        public Guid QuizId { get; set; }
        public Quiz<TUser> Quiz { get; set; }
        public ICollection<Answer<TUser>> Answers { get; set; }
    }
}
