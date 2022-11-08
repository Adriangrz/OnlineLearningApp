using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using OnlineLearningAppApi.Core.Entities;

namespace OnlineLearningAppApi.Infrastructure.Persistence
{
    public class User : IdentityUser, IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool SiteRules { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Token<User>> Tokens { get; set; }
        public ICollection<Team<User>> AddedTeams { get; set; }
        public ICollection<Team<User>> CreatedTeams { get; set; }
        public ICollection<Answer<User>> Answers { get; set; }
        public ICollection<Quiz<User>> Quizzes { get; set; }
        public List<UserQuiz<User>> UserQuizzes { get; set; }
        public UserImage<User> UserImage { get; set; }
    }
}
