﻿using OnlineLearningAppApi.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Models
{
    public class CreateQuizDto
    {
        public string Name { get; set; }
        public DateTime EndDate { get; set; }
    }
}