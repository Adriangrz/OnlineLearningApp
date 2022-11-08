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
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer<User>>
    {
        public void Configure(EntityTypeBuilder<Answer<User>> builder)
        {
            builder.HasOne(a=>a.Question).WithMany(q => q.Answers).HasForeignKey(a => a.QuestionId);
            builder.HasOne(a => a.User).WithMany(u => u.Answers).HasForeignKey(a => a.UserId);
        }
    }
}
