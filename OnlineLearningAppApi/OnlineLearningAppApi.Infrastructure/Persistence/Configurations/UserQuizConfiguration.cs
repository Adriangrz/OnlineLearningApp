using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearningAppApi.Core.Entities;

namespace OnlineLearningAppApi.Infrastructure.Persistence.Configurations
{
    public class UserQuizConfiguration : IEntityTypeConfiguration<UserQuiz<User>>
    {
        public void Configure(EntityTypeBuilder<UserQuiz<User>> builder)
        {
           /* builder.HasKey(uq => new { uq.QuizId, uq.UserId });

            builder.HasOne(uq=>uq.User).WithMany(uq=>uq.UserQuizzes).HasForeignKey(uq=>uq.UserId);

            builder.HasOne(uq => uq.Quiz).WithMany(uq => uq.UserQuizzes).HasForeignKey(uq => uq.QuizId);
            
            builder.Property(uq => uq.IsDone).HasDefaultValue(false);*/
        }
    }
}
