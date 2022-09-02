namespace OnlineLearningAppApi.Models
{
    public class ResponseTokenData
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
