using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Database.Entities;

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
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamImage> TeamsImages { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<QuestionImage> QuestionImages { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }

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
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
            };
            var userRole = new IdentityRole()
            {
                Name = "User",
                NormalizedName = "User".ToUpper(),
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
