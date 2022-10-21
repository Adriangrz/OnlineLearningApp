using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database.Entities
{
    public class Quiz
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public List<UserQuiz> UserQuizzes { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
