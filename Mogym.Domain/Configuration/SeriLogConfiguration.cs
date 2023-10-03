using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Configuration
{
    public class SeriLogConfiguration : IEntityTypeConfiguration<SeriLog>
    {
        public void Configure(EntityTypeBuilder<SeriLog> builder)
        {
            builder.ToTable("SeriLog");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Exception)
                .IsRequired(false);

            builder.Property(p => p.Properties)
                .IsRequired(false);

            builder.Property(p => p.LogEvent)
                .IsRequired(false);

            builder.Property(p => p.TimeStamp)
                .HasColumnType("datetime");

            builder.Property(p => p.Level)
                .HasMaxLength(16);
        }
    }
}
