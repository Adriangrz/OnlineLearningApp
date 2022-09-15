using Microsoft.AspNetCore.Identity;

namespace OnlineLearningAppApi.Database.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool SiteRules { get; set; }
        public string? ImagePath { get; set; }
        public ICollection<Token> Tokens { get; set; }
        public ICollection<Team> AddedTeams { get; set; }
        public ICollection<Team> CreatedTeams { get; set; }
        public UserImage UserImage { get; set; }
    }
}
