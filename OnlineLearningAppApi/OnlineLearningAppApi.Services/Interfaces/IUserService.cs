using OnlineLearningAppApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetByIdAsync(string id);
        Task<List<UserDto>> GetAllTeamMembersAsync(Guid teamId);
        Task<UserDto> AddUserToTeamAsync(Guid teamId, AddUserToTeamDto dto);
        Task DeleteUserFromTeamAsync(Guid teamId, string userId);
    }
}
