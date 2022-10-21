﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database.Entities
{
    public class TeamImage
    {
        public Guid Id { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
    }
}