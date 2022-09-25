using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineLearningAppApi.Database;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services.Authorization;
using OnlineLearningAppApi.Services.Exceptions;
using OnlineLearningAppApi.Services.Interfaces;
using System.Linq.Expressions;

namespace OnlineLearningAppApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public TeamService(ApplicationDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<Team> GetByIdAsync(Guid id)
        {
            var team = await _dbContext
               .Teams.Include(t => t.Admin)
               .FirstOrDefaultAsync(r => r.Id == id);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            return team;
        }

        public async Task<TeamDto> CreateAsync(CreateTeamDto dto)
        {
            var team = _mapper.Map<Team>(dto);
            team.AdminId = _userContextService.GetUserId;
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            team = await GetByIdAsync(team.Id);

            var teamDto = _mapper.Map<TeamDto>(team);
            return teamDto;
        }

        public async Task<List<TeamDto>> GetAllAsync(TeamQuery teamQuery)
        {
            var userId = _userContextService.GetUserId;

            var baseQuery = _dbContext
                .Teams
                .Include(t => t.Admin)
                .Where(t => (t.AdminId == userId || t.Users.Any(u => u.Id == userId)) && t.IsArchived == teamQuery.Archived);

            var teams = await baseQuery
                .ToListAsync();

            var teamsDtos = _mapper.Map<List<TeamDto>>(teams);

            return teamsDtos;
        }
        public async Task<TeamDto> UpdateAsync(Guid id, UpdateTeamDto dto)
        {
            var team = await GetByIdAsync(id);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            team.Name = dto.Name;
            team.IsArchived = dto.IsArchived;

            await _dbContext.SaveChangesAsync();

            var teamDto = _mapper.Map<TeamDto>(team);
            return teamDto;
        }

        public async Task<TeamDto> UpdateTeamNameAsync(Guid id, UpdateTeamNameDto dto)
        {
            var team = await GetByIdAsync(id);

            team.Name = dto.Name;

            var updateTeamDto = _mapper.Map<UpdateTeamDto>(team);

            var teamDto = await UpdateAsync(id,updateTeamDto);
            return teamDto;
        }

        public async Task<TeamDto> UpdateTeamIsArchivedAsync(Guid id, UpdateTeamIsArchivedDto dto)
        {
            var team = await GetByIdAsync(id);

            team.IsArchived = dto.IsArchived;

            var updateTeamDto = _mapper.Map<UpdateTeamDto>(team);

            var teamDto = await UpdateAsync(id, updateTeamDto);
            return teamDto;
        }
    }
}
