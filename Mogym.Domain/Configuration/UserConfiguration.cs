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

            builder.Property(x => x.FirstName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.LastName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.NationalCode).HasColumnType("varchar").HasMaxLength(10).IsRequired(false);
            builder.Property(x => x.UserName).HasColumnType("varchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.Password).HasColumnType("varchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.Gender).HasColumnType("int");
            builder.Property(x => x.Mobile).HasColumnType("varchar").HasMaxLength(11).IsRequired();
            builder.Property(x => x.Status).HasColumnType("int");
            builder.Property(x => x.UniqeUserName).HasColumnType("varchar").HasMaxLength(100).ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.BirthDay).HasColumnType("varchar").HasMaxLength(10).IsRequired(false);
            builder.Property(x => x.SmsConfirmCode).HasColumnType("varchar").HasMaxLength(5).IsRequired();
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(100).IsRequired(false);

        }
    }
}
