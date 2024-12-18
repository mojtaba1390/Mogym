﻿using Microsoft.EntityFrameworkCore;
using Mogym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mogym.Domain.Configuration
{
    public class FinanceConfiguration : IEntityTypeConfiguration<Finance>
    {
        public void Configure(EntityTypeBuilder<Finance> builder)
        {
            builder.ToTable("Finance");
            builder.Property(x => x.PlanId).HasColumnType("int").IsRequired();
            builder.Property(x => x.DiscountId).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.FinanceStatus).HasColumnType("int").IsRequired();
            builder.Property(x => x.TrainingPlanId).HasColumnType("int").IsRequired();
            builder.Property(x => x.FinalPrice).HasColumnType("float").IsRequired();




            builder.HasOne<Plan>(x => x.Plan_Finance)
                .WithMany(z => z.Finances)
                .HasForeignKey(a => a.PlanId)
                .IsRequired();

            builder.HasOne<Discount>(x => x.Discount_Finance)
                .WithMany(z => z.Finances)
                .HasForeignKey(a => a.DiscountId);
        }
    }
}
