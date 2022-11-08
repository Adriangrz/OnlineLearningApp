using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Entities
{
    public class UserQuiz<TUser> where TUser : IUser
    {
        public string UserId { get; set; }
        public TUser User { get; set; }
        public Guid QuizId { get; set; }
        public Quiz<TUser> Quiz { get; set; }
        public bool IsDone { get; set; }
    }
}
