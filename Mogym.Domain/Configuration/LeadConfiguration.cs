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
    public class LeadConfiguration:IEntityTypeConfiguration<Lead>
    {
        public void Configure(EntityTypeBuilder<Lead> builder)
        {
            builder.ToTable("Lead");
            //builder.HasQueryFilter(x => x.IsActive == EnumYesNo.Yes);

            builder.Property(x => x.Mobile).HasColumnType("char(11)").IsRequired();
            builder.Property(x => x.Message).HasColumnType("nvarchar(500)").IsRequired();
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)").IsRequired();
        }
    }
}
