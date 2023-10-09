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
    public class UserRoleConfiguration:IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");

            builder.Property(x => x.UserId).HasColumnType("int").IsRequired();
            builder.Property(x => x.Role).HasColumnType("int").IsRequired();


            builder.HasOne(x => x.Role)
                .WithMany(z => z.UserRoles)
                .HasForeignKey(a => a.RoleId);

            builder.HasOne(x => x.User)
                .WithMany(z => z.UserRoles)
                .HasForeignKey(a => a.UserId);
        }
    }
}
