using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningAppApi.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne<UserImage>(u => u.UserImage)
                .WithOne(ui => ui.User)
                .HasForeignKey<UserImage>(ui => ui.UserId);

            builder.Property(t => t.ImagePath).IsRequired(false);
        }
    }
}
