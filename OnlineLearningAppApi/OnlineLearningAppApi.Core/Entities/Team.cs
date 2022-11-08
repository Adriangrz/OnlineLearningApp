using Core.Interfaces;

namespace OnlineLearningAppApi.Core.Entities
{
    public class Team<TUser> where TUser : IUser
    {
        public Guid Id { get; set; }
        public string? ImagePath { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }
        public TUser Admin { get; set; }
        public bool IsArchived { get; set; }
        public ICollection<Quiz<TUser>> Quizzes { get; set; }
        public ICollection<TUser> Users { get; set; }
        public TeamImage<TUser> TeamImage { get; set; }
    }
}
