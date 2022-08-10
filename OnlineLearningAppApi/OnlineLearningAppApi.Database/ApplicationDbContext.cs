﻿using Microsoft.AspNetCore.Identity;
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
    public class ApplicationDbContext : IdentityDbContext<User>
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
            builder.Entity<User>().HasData(admin);
            var adminRole = new IdentityRole()
            {
                Name = AppRoles.Admin,
                NormalizedName = AppRoles.Admin.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            var userRole = new IdentityRole()
            {
                Name = AppRoles.User,
                NormalizedName = AppRoles.User.ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            builder.Entity<IdentityRole>().HasData(
                adminRole,
                userRole
            );

            builder.Entity<IdentityUserRole<string>>().HasData(
               new IdentityUserRole<string>() { RoleId = adminRole.Id, UserId = admin.Id },
               new IdentityUserRole<string>() { RoleId = userRole.Id, UserId = admin.Id }
            );
        }
    }
}
