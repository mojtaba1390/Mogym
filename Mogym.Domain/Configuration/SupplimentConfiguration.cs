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
    public class SupplimentConfiguration:IEntityTypeConfiguration<Suppliment>
    {
        public void Configure(EntityTypeBuilder<Suppliment> builder)
        {
            builder.ToTable("Suppliment");
            builder.Property(x => x.Title).HasColumnType("nvarchar(50)").IsRequired();
        }
    }
}
