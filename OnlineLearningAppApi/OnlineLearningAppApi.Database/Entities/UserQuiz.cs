using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database.Entities
{
    public class UserQuiz
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public bool IsDone { get; set; }
    }
}
