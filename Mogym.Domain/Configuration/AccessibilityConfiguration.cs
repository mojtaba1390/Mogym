using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Domain.Common;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Configuration
{
    public class AccessibilityConfiguration:IEntityTypeConfiguration<Accessibility>
    {
        public void Configure(EntityTypeBuilder<Accessibility> builder)
        {
            builder.ToTable("Accessibility");

            builder.Property(x => x.EnglishName).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.PersianName).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("int").HasDefaultValue(EnumYesNo.Yes).IsRequired();
            builder.Property(x => x.RoleId).HasColumnType("int").IsRequired();


            builder.HasOne<Role>(x => x.Role).WithMany(z => z.Accessibilities).HasForeignKey(a => a.RoleId);
            builder.HasOne<User>(x => x.User).WithMany(z => z.Accessibilities).HasForeignKey(a => a.UserId);

        }
    }
}
