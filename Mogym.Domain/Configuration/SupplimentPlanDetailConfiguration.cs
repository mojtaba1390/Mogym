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
    public class SupplimentPlanDetailConfiguration:IEntityTypeConfiguration<SupplimentPlanDetail>
    {
        public void Configure(EntityTypeBuilder<SupplimentPlanDetail> builder)
        {
            builder.ToTable("SupplimentPlanDetail");
            builder.Property(x => x.Amount).HasColumnType("float").IsRequired();
            builder.Property(x=>x.Scale).HasColumnType("int").IsRequired();

            builder.HasOne<Suppliment>(x => x.Suppliment_SupplimentPlanDetail)
                .WithMany(x => x.SupplimentPlanDetails)
                .HasForeignKey(x => x.SupplimentId)
                .IsRequired();

            builder.HasOne<SupplimentPlan>(x => x.SupplimentPlan_SupplimentPlanDetail)
                .WithMany(x => x.SupplimentPlanDetails)
                .HasForeignKey(x => x.SupplimentPlanId)
                .IsRequired();

        }
    }
}
