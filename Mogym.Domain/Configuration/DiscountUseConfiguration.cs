using Microsoft.EntityFrameworkCore;
using Mogym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mogym.Domain.Configuration
{
    public class DiscountUseConfiguration : IEntityTypeConfiguration<DiscountUse>
    {
        public void Configure(EntityTypeBuilder<DiscountUse> builder)
        {
            builder.ToTable("DiscountUse");
            builder.Property(x => x.UserId).HasColumnType("int").IsRequired();
            builder.Property(x => x.DiscountId).HasColumnType("int").IsRequired();
            builder.Property(x => x.UseDate).HasColumnType("datetime2").IsRequired();


        }
    }
}
