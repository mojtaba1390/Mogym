using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Abstractions;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Configuration
{
    public class AchievementConfiguration:IEntityTypeConfiguration<Achievement>
    {
        public void Configure(EntityTypeBuilder<Achievement> builder)
        {
            builder.ToTable("Achievement");
            builder.Property(x => x.Title).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Date).HasColumnType("int").IsRequired(false);

            builder.HasOne<UserProfile>(x => x.Achievement_UserProfile)
                .WithMany(z => z.Achievements)
                .HasForeignKey(a => a.UserProfileId)
                .IsRequired();
        }
    }
}
