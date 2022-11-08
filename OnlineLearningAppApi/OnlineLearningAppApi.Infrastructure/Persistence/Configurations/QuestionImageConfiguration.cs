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
    public class QuestionImageConfiguration : IEntityTypeConfiguration<QuestionImage<User>>
    {
        public void Configure(EntityTypeBuilder<QuestionImage<User>> builder)
        {
            builder.HasOne(qi => qi.Question).WithMany(q => q.QuestionImages).HasForeignKey(qi => qi.QuestionId);
        }
    }
}
