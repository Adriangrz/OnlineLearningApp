using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Entities
{
    public class Quiz<TUser> where TUser : IUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid TeamId { get; set; }
        public Team<TUser> Team { get; set; }
        public List<UserQuiz<TUser>> UserQuizzes { get; set; }
        public ICollection<Question<TUser>> Questions { get; set; }
        public ICollection<TUser> Users { get; set; }
    }
}
