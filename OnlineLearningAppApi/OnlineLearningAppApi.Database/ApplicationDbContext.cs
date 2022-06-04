using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database
{
    public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            GenerateAdminAndRoles(modelBuilder);
        }

        private void GenerateAdminAndRoles(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();

            var admin = new User()
            {
                Id = Guid.NewGuid(),
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "Antek",
                NormalizedUserName = "ANTEK",
                Surname = "Kowalski",
                Email = "admin@test.pl",
                NormalizedEmail = "ADMIN@TEST.PL",
                LockoutEnabled = true,
                EmailConfirmed = true,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PasswordHash = hasher.HashPassword(null, "Pa$$w0rd"),
                SiteRules = true,
            };
            builder.Entity<User>().HasData(admin);
            var adminRole = new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = AppRoles.Admin,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            var userRole = new IdentityRole<Guid>()
            {
                Id = Guid.NewGuid(),
                Name = AppRoles.User,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            builder.Entity<IdentityRole<Guid>>().HasData(
                adminRole,
                userRole
            );

            builder.Entity<IdentityUserRole<Guid>>().HasData(
               new IdentityUserRole<Guid>() { RoleId = adminRole.Id, UserId = admin.Id },
               new IdentityUserRole<Guid>() { RoleId = userRole.Id, UserId = admin.Id }
            );
        }
    }
}
