using AutoMapper;
using Core.Interfaces;
using Core.Mapper.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Exceptions;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Authorization;
using OnlineLearningAppApi.Infrastructure.Hubs;
using OnlineLearningAppApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly ITeamService _teamService;
        private readonly IHubContext<TeamHub> _teamHubContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public UserService(IMapper mapper, ApplicationDbContext dbContext, ITeamService teamService, IHubContext<TeamHub> teamHubContext, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _teamService = teamService;
            _teamHubContext = teamHubContext;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public async Task<UserDetailsDto> GetByIdAsync(string id)
        {
            var user = await _dbContext
               .Users
               .FirstOrDefaultAsync(r => r.Id == id);

            if (user is null)
                throw new NotFoundException("Użytkownik nie istnieje");

            var userDetailsDto = _mapper.Map<UserDetailsDto>(user);
            return userDetailsDto;
        }
        public async Task<List<UserDto>> GetAllTeamMembersAsync(Guid teamId)
        {
            var team = await GetTeamByIdAsync(teamId);

            var users = await _dbContext.Users.Where(u=>u.AddedTeams.Any(t=>t.Id==teamId)).ToListAsync();

            var usersDtos = _mapper.Map<List<UserDto>>(users);
            return usersDtos;
        }
        public async Task<List<QuizUserDto>> GetAllQuizMembersAsync(Guid quizId)
        {
            var quiz = await _dbContext.Quizzes.FirstOrDefaultAsync(q=>q.Id == quizId);
            if (quiz is null)
                throw new NotFoundException("Test nie istnieje");

            var team = await GetTeamByIdAsync(quiz.TeamId);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            var users = await _dbContext.UserQuizzes.Include(uq=>uq.User).Where(uq=>uq.QuizId==quizId).ToListAsync();

            var usersDtos = _mapper.Map<List<QuizUserDto>>(users);
            return usersDtos;
        }
        public async Task<UserDto> AddUserToTeamAsync(Guid teamId,AddUserToTeamDto dto)
        {
            var team = await GetTeamByIdAsync(teamId);
            var user = await _dbContext.Users.Include(u=>u.AddedTeams).FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user is null)
                throw new NotFoundException("Użytkownik nie istnieje");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            user.AddedTeams.Add(team);
            await _dbContext.SaveChangesAsync();

            var teamDto = _mapper.Map<TeamDto>(team);
            await _teamHubContext.Clients.User(user.Id).SendAsync("addToTeam", teamDto);

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task DeleteUserFromTeamAsync(Guid teamId, string userId)
        {
            var team = await GetTeamByIdAsync(teamId);
            var user = await _dbContext.Users.Include(u => u.AddedTeams).FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                throw new NotFoundException("Użytkownik nie istnieje");

            user.AddedTeams.Remove(team);
            await _dbContext.SaveChangesAsync();

            await _teamHubContext.Clients.User(user.Id).SendAsync("deleteFromTeam", team.Id);
        }

        private async Task<Team<User>> GetTeamByIdAsync(Guid id)
        {
            var team = await _dbContext
               .Teams
               .Include(t => t.Users)
               .Include(t => t.Admin)
               .FirstOrDefaultAsync(r => r.Id == id);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            return team;
        }
    }
}
