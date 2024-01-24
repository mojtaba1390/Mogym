using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Domain.Entities.Log;

namespace Mogym.Domain.Configuration.Log
{
    public class UserLoggingConfiguration:IEntityTypeConfiguration<UserLogging>
    {
        public void Configure(EntityTypeBuilder<UserLogging> builder)
        {
            builder.ToTable("UserLogging");

            builder.HasKey(p => p.Id);


            builder.Property(p => p.InsertDate)
                .IsRequired().ValueGeneratedOnAdd();

            builder.Property(p => p.Permalink).HasColumnType("nvarchar(300)")
                .IsRequired(false);

            builder.Property(p => p.Ip).HasColumnType("nvarchar(30)")
                .IsRequired(false);
            builder.Property(p => p.VisitorId).HasColumnType("nvarchar(60)")
                .IsRequired(false);

        }
    }
}
