﻿using OnlineLearningAppApi.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Models
{
    public class CreateQuestionDto
    {
        public string Text { get; set; }
        public AnswerType AnswerType { get; set; }
        public List<string>? MultipleChoiceOptions { get; set; }
        public string? CodeLanguage { get; set; }
        public string? Code { get; set; }
    }
}
