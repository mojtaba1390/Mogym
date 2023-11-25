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
    public class SupplimentPlanConfiguration:IEntityTypeConfiguration<SupplimentPlan>
    {
        public void Configure(EntityTypeBuilder<SupplimentPlan> builder)
        {
            builder.ToTable("SupplimentPlan");
            builder.Property(x=>x.Amount).HasColumnType("float").IsRequired();
            builder.Property(x => x.Scale).HasColumnType("int").IsRequired();


            builder.HasOne<Suppliment>(x => x.Suppliment_SupplimentPlan)
                .WithMany(x => x.SupplimentPlans)
                .HasForeignKey(x => x.SupplimentId)
                .IsRequired();

            builder.HasOne<Plan>(x => x.Plan_SupplimentPlan)
                .WithMany(x => x.SupplimentPlans)
                .HasForeignKey(x => x.PlanId)
                .IsRequired();
        }
    }
}
