using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Configuration
{
    public class WorkoutConfiguration:IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.ToTable("Workout");
            builder.Property(x => x.Title).HasColumnType("nvarchar(50)").IsRequired();


            builder.HasOne<Plan>(x => x.Plan_Workout)
                .WithMany(x => x.Workouts)
                .HasForeignKey(x => x.PlanId)
                .IsRequired();
        }
    }
}
