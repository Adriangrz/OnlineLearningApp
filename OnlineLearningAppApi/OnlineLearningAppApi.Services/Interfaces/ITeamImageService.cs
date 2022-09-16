using Microsoft.AspNetCore.Http;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface ITeamImageService
    {
        Task UploadAsync(Guid teamId, IFormFile file);
        Task<TeamImageDto> GetImageAsync(Guid teamId);
    }
}
