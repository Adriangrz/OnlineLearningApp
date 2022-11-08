using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningAppApi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne<UserImage<User>>(u => u.UserImage)
                .WithOne(ui => ui.User)
                .HasForeignKey<UserImage<User>>(ui => ui.UserId);

            builder.Property(t => t.ImagePath).IsRequired(false);
        }
    }
}
