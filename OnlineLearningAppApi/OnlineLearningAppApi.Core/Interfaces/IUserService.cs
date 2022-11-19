using Core.Interfaces;
using Core.Mapper.Dtos;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailsDto> GetByIdAsync(string id);
        Task<List<UserDto>> GetAllTeamMembersAsync(Guid teamId);
        Task<UserDto> AddUserToTeamAsync(Guid teamId, AddUserToTeamDto dto);
        Task DeleteUserFromTeamAsync(Guid teamId, string userId);
        Task<List<QuizUserDto>> GetAllQuizMembersAsync(Guid quizId);
    }
}
