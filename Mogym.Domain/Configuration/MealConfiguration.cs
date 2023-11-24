﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Configuration
{
    public class MealConfiguration:IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.ToTable("Meal");
            builder.Property(x => x.Title).HasColumnType("nvarchar(50)").IsRequired();


            builder.HasOne<Plan>(x => x.Plan_Meal)
                .WithMany(x => x.Meals)
                .HasForeignKey(x => x.PlanId)
                .IsRequired();
        }
    }
}
