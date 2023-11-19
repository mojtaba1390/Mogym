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
    public class ExerciseConfiguration:IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercise");

            builder.HasOne<Exercise>(x => x.Exercise_Exercise)
                .WithMany(x => x.Exercises)
                .HasForeignKey(x => x.SuperSetId)
                .IsRequired(false);

            builder.HasOne<Workout>(x => x.Exercise_Workout)
                .WithMany(x => x.Exercises)
                .HasForeignKey(x => x.WorkoutId)
                .IsRequired();
        }
    }
}
