using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Models
{
    public class User : IdentityUser<Guid>
    {
        public string Surname { get; set; }
        public bool SiteRules { get; set; }
    }
}
