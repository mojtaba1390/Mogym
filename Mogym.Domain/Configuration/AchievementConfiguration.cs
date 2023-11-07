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
    public class AchievementConfiguration:IEntityTypeConfiguration<TrainerAchievement>
    {
        public void Configure(EntityTypeBuilder<TrainerAchievement> builder)
        {
            builder.ToTable("TrainerAchievement");
            builder.Property(x => x.Title).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Date).HasColumnType("int").IsRequired(false);

            builder.HasOne<TrainerProfile>(x => x.TrainerAchievement_TrainerProfile)
                .WithMany(z => z.TrainerAchievements)
                .HasForeignKey(a => a.TrainerProfileId)
                .IsRequired();
        }
    }
}
