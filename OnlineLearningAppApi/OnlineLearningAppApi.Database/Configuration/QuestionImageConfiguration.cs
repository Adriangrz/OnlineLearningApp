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
    public class QuestionImageConfiguration : IEntityTypeConfiguration<QuestionImage>
    {
        public void Configure(EntityTypeBuilder<QuestionImage> builder)
        {
            builder.HasOne(qi => qi.Question).WithMany(q => q.QuestionImages).HasForeignKey(qi => qi.QuestionId);
        }
    }
}
