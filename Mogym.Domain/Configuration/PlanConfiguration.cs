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
    public class PlanConfiguration:IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Plan");

            builder.Property(x => x.PlanStatus).HasColumnType("int").IsRequired();
            builder.Property(x => x.PaidPicture).HasColumnType("nvarchar(50)").IsRequired(false);
            builder.Property(x => x.TrackingCode).HasColumnType("int").IsRequired();
            builder.Property(x => x.SendPlanDate).HasColumnType("datetime2").IsRequired(false);


            builder.HasOne<TrainerProfile>(x => x.TrainerProfile_Plan)
                .WithMany(z => z.Plans)
                .HasForeignKey(a => a.TrainerId)
                .IsRequired();

            builder.HasOne<User>(x => x.User_Plan)
                .WithMany(z => z.Plans)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

            builder.HasOne<Question>(x => x.AnsweQuestion_Plan)
                .WithMany(z => z.Plans)
                .HasForeignKey(a => a.AnserQuestionId)
                .IsRequired();
        }
    }
}
