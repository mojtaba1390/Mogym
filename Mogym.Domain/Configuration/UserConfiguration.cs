using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Configuration
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(o => o.Id);


            builder.Property(x => x.FirstName).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(x => x.LastName).HasColumnType("varchar").HasMaxLength(200);
            builder.Property(x => x.UserName).HasColumnType("varchar").HasMaxLength(200);
            builder.Property(x => x.Gender).HasColumnType("int");

        }
    }
}
