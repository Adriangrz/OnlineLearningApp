using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningAppApi.Core.Entities;

namespace OnlineLearningAppApi.Infrastructure.Persistence.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team<User>>
    {
        public void Configure(EntityTypeBuilder<Team<User>> builder)
        {
            builder.HasMany(t => t.Users)
                .WithMany(u => u.AddedTeams)
                .UsingEntity<Dictionary<string, object>>(
                    "UserTeams",
                    j => j
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId"),
                    j => j
                        .HasOne<Team<User>>()
                        .WithMany()
                        .HasForeignKey("TeamId")
                );

            builder.HasOne(t => t.Admin)
                .WithMany(u => u.CreatedTeams)
                .HasForeignKey(t => t.AdminId).OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<TeamImage<User>>(t => t.TeamImage)
                .WithOne(ti => ti.Team)
                .HasForeignKey<TeamImage<User>>(ti => ti.TeamId);

            builder.Property(t => t.IsArchived).HasDefaultValue(false);
            builder.Property(t => t.ImagePath).IsRequired(false);
        }
    }
}
