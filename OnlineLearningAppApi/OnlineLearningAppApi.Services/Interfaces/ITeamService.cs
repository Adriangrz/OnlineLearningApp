using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;

namespace OnlineLearningAppApi.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> CreateAsync(CreateTeamDto dto);
        Task<PagedResult<TeamDto>> GetAllAsync(TeamQuery query);
        Task<TeamDto> UpdateAsync(Guid id, UpdateTeamDto dto);
    }
}
