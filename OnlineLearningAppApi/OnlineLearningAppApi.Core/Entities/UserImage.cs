using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Entities
{
    public class UserImage<TUser> where TUser : IUser
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string UserId { get; set; }
        public TUser User { get; set; }
    }
}
