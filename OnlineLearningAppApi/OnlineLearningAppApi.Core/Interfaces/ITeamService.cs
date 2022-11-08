using OnlineLearningAppApi.Core.Mapper.Dtos;

namespace OnlineLearningAppApi.Core.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> GetByIdAsync(Guid id);
        Task<TeamDto> CreateAsync(CreateTeamDto dto);
        Task<List<TeamDto>> GetAllAsync(TeamQuery teamQuery);
        Task<TeamDto> UpdateAsync(Guid id, UpdateTeamDto dto);
        Task<TeamDto> UpdateTeamIsArchivedAsync(Guid id, UpdateTeamIsArchivedDto dto);
        Task<TeamDto> UpdateTeamNameAsync(Guid id, UpdateTeamNameDto dto);
    }
}
