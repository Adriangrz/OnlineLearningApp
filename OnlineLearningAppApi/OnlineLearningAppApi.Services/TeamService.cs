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
        public async Task<TeamDto> CreateAsync(CreateTeamDto dto)
        {
            var team = _mapper.Map<Team>(dto);
            team.AdminId = _userContextService.GetUserId;
            await _dbContext.Teams.AddAsync(team);
            await _dbContext.SaveChangesAsync();

            var teamDto = _mapper.Map<TeamDto>(team);
            return teamDto;
        }

        public async Task<PagedResult<TeamDto>> GetAllAsync(TeamQuery query)
        {
            var userId = _userContextService.GetUserId;

            var baseQuery = _dbContext
                .Teams
                .Where(t => t.AdminId == userId || t.Users.Any(u => u.Id == userId));

            var teams = await baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .ToListAsync();

            var totalItemsCount = await baseQuery.CountAsync();

            var teamsDtos = _mapper.Map<List<TeamDto>>(teams);

            var result = new PagedResult<TeamDto>(teamsDtos, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }
        public async Task<TeamDto> UpdateAsync(Guid id, UpdateTeamDto dto)
        {
            var team = await _dbContext
                .Teams
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
    }
}
