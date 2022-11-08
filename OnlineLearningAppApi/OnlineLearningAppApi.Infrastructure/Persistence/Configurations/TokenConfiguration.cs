using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningAppApi.Core.Entities;

namespace OnlineLearningAppApi.Infrastructure.Persistence.Configurations
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token<User>>
    {
        public void Configure(EntityTypeBuilder<Token<User>> builder)
        {
            builder.HasOne<User>(t => t.User)
               .WithMany(u => u.Tokens)
               .HasForeignKey(t => t.UserId);
        }
    }
}
