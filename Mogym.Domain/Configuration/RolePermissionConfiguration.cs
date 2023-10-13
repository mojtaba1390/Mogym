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
    public class RolePermissionConfiguration:IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermission");

            builder.Property(x => x.RoleId).HasColumnType("int").IsRequired();
            builder.Property(x => x.PermissionId).HasColumnType("int").IsRequired();


            builder.HasOne(x => x.RolePermission_Role)
                .WithMany(z => z.RolePermissions)
                .HasForeignKey(a => a.RoleId);

            builder.HasOne(x => x.RolePermission_Permission)
                .WithMany(z => z.RolePermissions)
                .HasForeignKey(a => a.PermissionId);
        }
    }
}
