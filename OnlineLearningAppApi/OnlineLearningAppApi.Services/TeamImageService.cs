﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Database;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Models;
using OnlineLearningAppApi.Services.Authorization;
using OnlineLearningAppApi.Services.Exceptions;
using OnlineLearningAppApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services
{
    public class TeamImageService : ITeamImageService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public TeamImageService(IMapper mapper, ApplicationDbContext dbContext, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
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
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                if (memoryStream.Length >= 2097152)
                    throw new BadRequestException("Rozmiar pliku musi być mniejszy niż 2 MB");

                var teamImage = new TeamImage()
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
            var team = await _dbContext
                .Teams
                .FirstOrDefaultAsync(r => r.Id == teamId);

            if (team is null)
                throw new NotFoundException("Zespół nie istnieje");


            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, team.AdminId,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var image = await _dbContext
               .TeamsImages
               .FirstOrDefaultAsync(r => r.TeamId == teamId);

            if (image is null)
                throw new NotFoundException("Zespół nie posiada obrazu");

            var teamImageDto = _mapper.Map<TeamImageDto>(image);
            return teamImageDto;
        }
    }
}
