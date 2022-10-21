using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OnlineLearningAppApi.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database.Configuration
{
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.HasOne(q => q.Team).WithMany(t => t.Quizzes).HasForeignKey(q => q.TeamId);

            builder.HasMany(p => p.Users)
            .WithMany(p => p.Quizzes)
            .UsingEntity<UserQuiz>(
                j => j
                    .HasOne(pt => pt.User)
                    .WithMany(t => t.UserQuizzes)
                    .HasForeignKey(pt => pt.UserId),
                j => j
                    .HasOne(pt => pt.Quiz)
                    .WithMany(p => p.UserQuizzes)
                    .HasForeignKey(pt => pt.QuizId),
                j =>
                {
                    j.Property(pt => pt.IsDone).HasDefaultValue(false);
                    j.HasKey(t => new { t.UserId, t.QuizId });
                });
        }
    }
}
