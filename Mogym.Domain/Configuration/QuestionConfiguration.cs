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
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");
            builder.Property(x => x.FirstName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(false);
            builder.Property(x => x.LastName).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.Gender).HasColumnType("int");
            builder.Property(x => x.Age).HasColumnType("int");
            builder.Property(x => x.Height).HasColumnType("int");
            builder.Property(x => x.Weight).HasColumnType("float");
            builder.Property(x => x.Waist).HasColumnType("float");
            builder.Property(x => x.Biceps).HasColumnType("float");
            builder.Property(x => x.Chest).HasColumnType("float");
            builder.Property(x => x.Thigh).HasColumnType("float");
            builder.Property(x => x.Fist).HasColumnType("float");
            builder.Property(x => x.DailyAvtivity).HasColumnType("int");
            builder.Property(x => x.NightWork).HasColumnType("int");
            builder.Property(x => x.Disease).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.Medicine).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.Treated).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.Injury).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.HeartDisease).HasColumnType("int");
            builder.Property(x => x.DiabetesAsthmaHypoglycemia).HasColumnType("int");
            builder.Property(x => x.Smoke).HasColumnType("int");
            builder.Property(x => x.SessionsInWeek).HasColumnType("int");
            builder.Property(x => x.Expection).HasColumnType("nvarchar").HasMaxLength(200).IsRequired(false);
            builder.Property(x => x.FrontPic).HasColumnType("nvarchar").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.BackPic).HasColumnType("nvarchar").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.LeftPic).HasColumnType("nvarchar").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.RightPic).HasColumnType("nvarchar").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.TrainerPlan).HasColumnType("int");

        }
    }
}
