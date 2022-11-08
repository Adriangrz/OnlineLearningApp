using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Mapper.Dtos
{
    public class TeamImageDto
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
