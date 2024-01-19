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
    public class TrainerProfileConfiguration:IEntityTypeConfiguration<TrainerProfile>
    {
        public void Configure(EntityTypeBuilder<TrainerProfile> builder)
        {
            builder.ToTable("TrainerProfile");
            builder.Property(x => x.Biography).HasColumnType("nvarchar(1000)").IsRequired(false);
            builder.Property(x => x.CartNumber).HasColumnType("char(19)").IsRequired(false);
            builder.Property(x => x.CartOwnerName).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(x => x.NationalCartPic).HasColumnType("nvarchar(200)").IsRequired(false);
            builder.Property(x => x.TrainingCertificatePic).HasColumnType("nvarchar(200)").IsRequired(false);
            builder.Property(x => x.WorkingCertificatePic).HasColumnType("nvarchar(200)").IsRequired(false);
            builder.Property(x => x.ChampCertificatePic1).HasColumnType("nvarchar(200)").IsRequired(false);
            builder.Property(x => x.ChampCertificatePic2).HasColumnType("nvarchar(200)").IsRequired(false);
            builder.Property(x => x.ChampCertificatePic3).HasColumnType("nvarchar(200)").IsRequired(false);
            builder.Property(x => x.GraduationCertificatePic).HasColumnType("nvarchar(200)").IsRequired(false);
            builder.Property(x => x.StudingCertificatePic).HasColumnType("nvarchar(200)").IsRequired(false);



        }
    }
}
