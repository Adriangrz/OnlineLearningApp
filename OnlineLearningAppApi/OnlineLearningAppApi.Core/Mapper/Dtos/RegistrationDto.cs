namespace OnlineLearningAppApi.Core.Mapper.Dtos
{
    public class RegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool SiteRules { get; set; }
    }
}
