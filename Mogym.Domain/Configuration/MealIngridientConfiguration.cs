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
    public class MealIngridientConfiguration:IEntityTypeConfiguration<MealIngridient>
    {
        public void Configure(EntityTypeBuilder<MealIngridient> builder)
        {
            builder.ToTable("MealIngridient");
            builder.Property(x => x.Count).HasColumnType("float").IsRequired(false);
            builder.Property(x => x.Size).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Amount).HasColumnType("float").IsRequired(false);

            builder.HasOne<Meal>(x => x.Meal_MealIngridient)
                .WithMany(x => x.MealIngridients)
                .HasForeignKey(x => x.MealId)
                .IsRequired();

            builder.HasOne<Ingredient>(x => x.Ingredient_MealIngridient)
                .WithMany(x => x.MealIngridients)
                .HasForeignKey(x => x.IngridientId)
                .IsRequired();


        }
    }
}
