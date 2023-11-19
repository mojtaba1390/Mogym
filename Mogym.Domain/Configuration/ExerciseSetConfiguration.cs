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
    public class ExerciseSetConfiguration:IEntityTypeConfiguration<ExerciseSet>
    {
        public void Configure(EntityTypeBuilder<ExerciseSet> builder)
        {
            builder.ToTable("ExerciseSet");
            builder.Property(x => x.Weight).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Count).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Minute).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Second).HasColumnType("int").IsRequired(false);

            builder.HasOne<Exercise>(x => x.Exercise_ExerciseSet)
                .WithMany(x => x.ExerciseSets)
                .HasForeignKey(x => x.ExerciseId)
                .IsRequired();

        }
    }
}
