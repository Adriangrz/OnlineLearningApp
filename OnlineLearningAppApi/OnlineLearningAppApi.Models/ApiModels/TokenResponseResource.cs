using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Models.ApiModels
{
    public class TokenResponseResource
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
