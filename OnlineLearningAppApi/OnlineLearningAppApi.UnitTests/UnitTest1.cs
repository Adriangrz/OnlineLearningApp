using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using OnlineLearningAppApi.Core.Entities;
using OnlineLearningAppApi.Core.Interfaces;
using OnlineLearningAppApi.Core.Mapper.Dtos;
using OnlineLearningAppApi.Infrastructure.Persistence;
using OnlineLearningAppApi.Infrastructure.Services;
using System.Collections.Generic;

namespace OnlineLearningAppApi.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test2Async()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Products Test").Options;
            var context = new ApplicationDbContext(options);
            var hasher = new PasswordHasher<User>();
            var admin = new User()
            {
                UserName = "admin@test.pl",
                NormalizedUserName = "ADMIN@TEST.PL",
                FirstName = "Antek",
                LastName = "Kowalski",
                Email = "admin@test.pl",
                NormalizedEmail = "ADMIN@TEST.PL",
                LockoutEnabled = true,
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                SiteRules = true,
            };
            context.Users.Add(admin);
            context.SaveChanges();
            var user = context.Users.FirstOrDefault(x => x.Email == admin.Email);
            Mock<IMapper> mapper = new Mock<IMapper>();
            mapper.Setup(x => x.Map<Team<User>>(It.IsAny<CreateTeamDto>()))
                .Returns((CreateTeamDto source) =>
                {
                    var team = new Team<User>()
                    {
                        Name = source.Name,
                    };

                    return team;
                });
            mapper.Setup(x => x.Map<TeamDto>(It.IsAny<Team<User>>()))
                .Returns((Team<User> source) =>
                {
                    var teamDto = new TeamDto()
                    {
                        Id = source.Id,
                        Name = source.Name,
                    };

                    return teamDto;
                });
            var usercontextserv = new Mock<IUserContextService>();
            usercontextserv.Setup(u => u.GetUserId).Returns(user.Id.ToString());

            TeamService teamService = new TeamService(context, mapper.Object,null, usercontextserv.Object,null);
            var createTeamDto = new CreateTeamDto()
            {
                Name = "testowy"
            };
            var team = await teamService.CreateAsync(createTeamDto);
            Assert.IsNotNull(team);
        }
    }
}