namespace OnlineLearningAppApi.Core.Mapper.Dtos
{
    public class ResponseTokenDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
