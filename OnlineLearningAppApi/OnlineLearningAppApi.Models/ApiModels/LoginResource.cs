using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Models.ApiModels
{
    public class LoginResource
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ClientId { get; set; }
    }
}
