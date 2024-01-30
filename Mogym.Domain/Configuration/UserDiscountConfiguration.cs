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
    public class UserDiscountConfiguration : IEntityTypeConfiguration<UserDiscount>
    {
        public void Configure(EntityTypeBuilder<UserDiscount> builder)
        {
            builder.ToTable("UserDiscount");

            builder.Property(x => x.UserId).HasColumnType("int").IsRequired();
            builder.Property(x => x.DiscountId).HasColumnType("int").IsRequired();


            builder.HasOne(x => x.UserDiscount_User)
                .WithMany(z => z.UserDiscounts)
                .HasForeignKey(a => a.UserId);

            builder.HasOne(x => x.UserDiscount_Discount)
                .WithMany(z => z.UserDiscounts)
                .HasForeignKey(a => a.DiscountId);
        }
    }
}
