using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningAppApi.Database.Entities;

namespace OnlineLearningAppApi.Database.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
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
                        .HasOne<Team>()
                        .WithMany()
                        .HasForeignKey("TeamId")
                );

            builder.HasOne(t => t.Admin)
                .WithMany(u => u.CreatedTeams)
                .HasForeignKey(t => t.AdminId).OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<TeamImage>(t => t.TeamImage)
                .WithOne(ti => ti.Team)
                .HasForeignKey<TeamImage>(ti => ti.TeamId);

            builder.Property(t => t.IsArchived).HasDefaultValue(false);
            builder.Property(t => t.ImagePath).IsRequired(false);
        }
    }
}
