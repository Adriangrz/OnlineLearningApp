namespace OnlineLearningAppApi.Database.Entities
{
    public class Team
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }
        public User Admin { get; set; }
        public bool IsArchived { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
