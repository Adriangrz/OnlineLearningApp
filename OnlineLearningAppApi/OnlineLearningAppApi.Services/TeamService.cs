using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using OnlineLearningAppApi.Database;
using OnlineLearningAppApi.Database.Entities;
using OnlineLearningAppApi.Services.Interfaces;

namespace OnlineLearningAppApi.Services
{
    public class TeamService : ITeamService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public TeamService(ApplicationDbContext dbContext, UserManager<User> userManager, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
    }
}
