using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database.Entities
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public string? Code { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
