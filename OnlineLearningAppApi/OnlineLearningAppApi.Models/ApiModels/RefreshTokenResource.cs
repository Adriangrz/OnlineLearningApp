using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Models.ApiModels
{
    public class RefreshTokenResource
    {
        public string ClientId { get; set; }
        public string RefreshToken { get; set; }
    }
}
