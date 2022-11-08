using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Core.Entities;

namespace OnlineLearningAppApi.Infrastructure.Persistence.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question<User>>
    {
        public void Configure(EntityTypeBuilder<Question<User>> builder)
        {
            builder.HasOne(q => q.Quiz).WithMany(q => q.Questions).HasForeignKey(q => q.QuizId);
            builder.Property(q => q.MultipleChoiceOptions).HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());

            builder.Property(q => q.ImagePaths).HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
