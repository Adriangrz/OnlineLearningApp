﻿namespace OnlineLearningAppApi.Models
{
    public class RequestTokenData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string RefreshToken { get; set; }
    }
}
