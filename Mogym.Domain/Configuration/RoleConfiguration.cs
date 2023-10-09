﻿using System;
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
    public class RoleConfiguration:IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.Property(x => x.EnglishName).HasColumnType("varchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.PersianName).HasColumnType("nvarchar").HasMaxLength(150).IsRequired();
            builder.Property(x => x.IsActive).HasColumnType("int").HasDefaultValue(EnumYesNo.Yes).IsRequired();


            builder.HasOne<User>(x => x.User).WithMany(z => z.Roles).HasForeignKey(a => a.UserId);

        }
    }
}
