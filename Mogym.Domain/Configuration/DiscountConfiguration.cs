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
    public class DiscountConfiguration:IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.ToTable("Discount");
            builder.Property(x => x.DiscountText).HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(x => x.DiscountType).HasColumnType("int").IsRequired();
            builder.Property(x => x.Value).HasColumnType("int").IsRequired();
            builder.Property(x => x.ActiveDate).HasColumnType("datetime2").IsRequired();

            builder.HasOne<TrainerProfile>(x => x.TrainerProfile)
                .WithMany(x => x.Discounts)
                .HasForeignKey(u => u.TrainerId)
                .IsRequired(false);

        }
    }
}
