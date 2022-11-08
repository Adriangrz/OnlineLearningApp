using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Entities
{
    public class Answer<TUser> where TUser : IUser
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string? CodeLanguage { get; set; }
        public string? Code { get; set; }
        public Guid QuestionId { get; set; }
        public Question<TUser> Question { get; set; }
        public string UserId { get; set; }
        public TUser User { get; set; }
    }
}
