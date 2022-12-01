using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Mapper.Dtos
{
    public class QuizDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid TeamId { get; set; }
        public bool isUserAssigned { get; set; }
        public bool? IsDone { get; set; } = null;
    }
}
