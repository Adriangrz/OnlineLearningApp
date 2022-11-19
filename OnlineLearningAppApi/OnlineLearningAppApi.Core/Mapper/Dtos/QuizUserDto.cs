using Core.Interfaces;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper.Dtos
{
    public class QuizUserDto
    {
        public UserDto User { get; set; }
        public Guid QuizId { get; set; }
        public bool IsDone { get; set; }
    }
}
