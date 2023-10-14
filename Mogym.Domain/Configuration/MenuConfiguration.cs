using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Domain.Common;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Configuration
{
    public class MenuConfiguration:IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menu");
            builder.HasQueryFilter(x => x.IsActive == EnumYesNo.Yes);

            builder.Property(x => x.EnglishName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.PersianName).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Link).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("int").IsRequired();


            builder.HasOne<Menu>(x => x.Menu_Menu).WithMany(z => z.Menus).HasForeignKey(a => a.ParentId)
                .IsRequired(false);
        }
    }
}
