using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database.Entities
{
    public class UserImage
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
