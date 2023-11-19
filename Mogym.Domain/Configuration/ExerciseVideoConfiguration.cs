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
    public class ExerciseVideoConfiguration:IEntityTypeConfiguration<ExerciseVideo>
    {
        public void Configure(EntityTypeBuilder<ExerciseVideo> builder)
        {
            builder.ToTable("ExerciseVideo");
            builder.Property(x => x.Title).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("nvarchar(500)").IsRequired(false);
            builder.Property(x => x.FileName).HasColumnType("nvarchar(100)").IsRequired(false);


            builder.HasOne<Exercise>(u => u.Exercise)
                .WithOne(p => p.ExerciseVideo)
                .HasForeignKey<Exercise>(p => p.ExerciseVideoId)
                .IsRequired(false);

        }
    }
}
