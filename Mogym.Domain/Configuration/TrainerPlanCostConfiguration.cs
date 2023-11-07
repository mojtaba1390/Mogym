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
    public class TrainerPlanCostConfiguration:IEntityTypeConfiguration<TrainerPlanCost>
    {
        public void Configure(EntityTypeBuilder<TrainerPlanCost> builder)
        {
            builder.ToTable("TrainerPlanCost");
            builder.Property(x => x.TrainerPlan).HasColumnType("int").IsRequired();
            builder.Property(x => x.OriginalCost).HasColumnType("float").IsRequired(false);
            builder.Property(x => x.SaleCost).HasColumnType("float").IsRequired(false);

            builder.HasOne<TrainerProfile>(x => x.TrainerPlanCost_TrainerProfile)
                .WithMany(z => z.TrainerPlanCosts)
                .HasForeignKey(a => a.TrainerProfileId)
                .IsRequired();
        }
    }
}
