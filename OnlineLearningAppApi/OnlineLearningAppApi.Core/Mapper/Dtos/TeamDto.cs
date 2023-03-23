namespace OnlineLearningAppApi.Core.Mapper.Dtos
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string? ImagePath { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }
        public string Email { get; set; }
        public bool IsArchived { get; set; }
        public UserDto Admin { get; set; }
    }
}
