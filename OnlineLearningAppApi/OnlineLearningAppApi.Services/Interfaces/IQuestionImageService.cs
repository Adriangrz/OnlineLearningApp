﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface IQuestionImageService
    {
        Task UploadAsync(Guid questionId, IFormFile file);
    }
}
