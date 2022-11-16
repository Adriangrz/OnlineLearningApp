using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Exceptions;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Authorization;
using OnlineLearningAppApi.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Infrastructure.Services
{
    public class TeamImageService : ITeamImageService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly ITeamService _teamService;
        public TeamImageService(IMapper mapper, ApplicationDbContext dbContext, IAuthorizationService authorizationService, IUserContextService userContextService, ITeamService teamService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _teamService = teamService;
        }
        public async Task UploadAsync(Guid teamId, IFormFile file)
        {
            if (file == null || file.Length == 0 || !file.ContentType.StartsWith("image"))
                throw new BadRequestException("Plik jest wymagany i musi to być obraz");

            var team = await _dbContext
                .Teams
                .FirstOrDefaultAsync(r => r.Id == teamId);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");


            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                if (memoryStream.Length >= 2097152)
                    throw new BadRequestException("Rozmiar pliku musi być mniejszy niż 2 MB");

                var teamImage = new TeamImage<User>()
                {
                    Content = memoryStream.ToArray(),
                    ContentType = file.ContentType,
                    TeamId = team.Id
                };
                await _dbContext.TeamsImages.AddAsync(teamImage);

                team.ImagePath = $"api/Team/{teamId}/TeamImage";

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TeamImageDto> GetImageAsync(Guid teamId)
        {
            var team = await GetTeamByIdAsync(teamId);

            var image = await _dbContext
               .TeamsImages
               .FirstOrDefaultAsync(r => r.TeamId == teamId);

            if (image is null)
                throw new NotFoundException("Zespół nie posiada obrazu");

            var teamImageDto = _mapper.Map<TeamImageDto>(image);
            return teamImageDto;
        }

        private async Task<Team<User>> GetTeamByIdAsync(Guid id)
        {
            var team = await _dbContext
               .Teams
               .FirstOrDefaultAsync(r => r.Id == id);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");

            return team;
        }
    }
}
