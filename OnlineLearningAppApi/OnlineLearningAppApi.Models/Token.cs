using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Models
{
    public class Token
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public int Type { get; set; }
        public string Value { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public User? User { get; set; }
    }
}
