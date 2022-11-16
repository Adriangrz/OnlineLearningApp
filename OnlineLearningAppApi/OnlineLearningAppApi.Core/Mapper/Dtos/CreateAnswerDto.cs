using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper.Dtos
{
    public class CreateAnswerDto
    {
        public string Value { get; set; }
        public string? CodeLanguage { get; set; }
        public string? Code { get; set; }
    }
}
