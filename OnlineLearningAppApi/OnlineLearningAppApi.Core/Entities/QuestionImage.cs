using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Entities
{
    public class QuestionImage<TUser> where TUser : IUser
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public Guid QuestionId { get; set; }
        public Question<TUser> Question { get; set; }
    }
}
