using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Database;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services.Exceptions;
using OnlineLearningAppApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly ITeamService _teamService;

        public UserService(IMapper mapper, ApplicationDbContext dbContext, ITeamService teamService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _teamService = teamService;
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
            var team = await _teamService.GetByIdAsync(teamId);

            var users = await _dbContext.Users.Where(u=>u.AddedTeams.Any(t=>t.Id==teamId)).ToListAsync();

            var usersDtos = _mapper.Map<List<UserDto>>(users);
            return usersDtos;
        }
        public async Task<UserDto> AddUserToTeamAsync(Guid teamId,AddUserToTeamDto dto)
        {
            var team = await _teamService.GetByIdAsync(teamId);
            var user = await _dbContext.Users.Include(u=>u.AddedTeams).FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user is null)
                throw new NotFoundException("Użytkownik nie istnieje");

            user.AddedTeams.Add(team);
            await _dbContext.SaveChangesAsync();

            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task DeleteUserFromTeamAsync(Guid teamId, string userId)
        {
            var team = await _teamService.GetByIdAsync(teamId);
            var user = await _dbContext.Users.Include(u => u.AddedTeams).FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                throw new NotFoundException("Użytkownik nie istnieje");

            user.AddedTeams.Remove(team);
            await _dbContext.SaveChangesAsync();

        }
    }
}
