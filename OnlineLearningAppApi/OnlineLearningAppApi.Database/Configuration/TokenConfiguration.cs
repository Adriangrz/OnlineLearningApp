using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningAppApi.Database.Entities;

namespace OnlineLearningAppApi.Database.Configuration
{
    public class TokenConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasOne<User>(t => t.User)
               .WithMany(u => u.Tokens)
               .HasForeignKey(t => t.UserId);
        }
    }
}
