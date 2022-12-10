using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Exceptions;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Authorization;
using OnlineLearningAppApi.Infrastructure.Hubs;
using OnlineLearningAppApi.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace OnlineLearningAppApi.Infrastructure.Services
{
    public class TeamService : ITeamService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly IHubContext<TeamHub> _teamHubContext;

        public TeamService(ApplicationDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService, IHubContext<TeamHub> teamHubContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _teamHubContext = teamHubContext;
        }

        public async Task<TeamDto> GetByIdAsync(Guid id)
        {
            var team = await _dbContext
               .Teams
               .Include(t => t.Admin)
               .Include(t=>t.Users)
               .FirstOrDefaultAsync(r => r.Id == id);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var teamDto = _mapper.Map<TeamDto>(team);
            return teamDto;
        }

        public async Task<TeamDto> CreateAsync(CreateTeamDto dto)
        {
            var team = _mapper.Map<Team<User>>(dto);
            team.AdminId = _userContextService.GetUserId;
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            team = await _dbContext
                .Teams
                .Include(t => t.Admin)
                .FirstOrDefaultAsync(r => r.Id == team.Id);

            if (team is null)
                throw new NotFoundException("Nie udało się utworzyć zespołu");

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
            var team = await _dbContext
                .Teams
                .Include(t => t.Admin)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

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
            var team = await _dbContext
                .Teams
                .Include(t => t.Admin)
                .Include(t => t.Users)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            team.Name = dto.Name;

            await _dbContext.SaveChangesAsync();


            var teamDto = _mapper.Map<TeamDto>(team);
            await _teamHubContext.Clients.Users(team.Users.Select(u => u.Id)).SendAsync("changeTeamName", teamDto);
            return teamDto;
        }

        public async Task<TeamDto> UpdateTeamIsArchivedAsync(Guid id, UpdateTeamIsArchivedDto dto)
        {
            var team = await _dbContext
                .Teams
                .Include(t => t.Admin)
                .Include(t=>t.Users)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            team.IsArchived = dto.IsArchived;

            await _dbContext.SaveChangesAsync();


            var teamDto = _mapper.Map<TeamDto>(team);
            await _teamHubContext.Clients.Users(team.Users.Select(u => u.Id)).SendAsync("changeTeamIsArchived", teamDto);
            return teamDto;
        }
    }
}
