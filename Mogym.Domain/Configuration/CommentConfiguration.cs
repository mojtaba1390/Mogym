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
    public class CommentCofiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
            builder.Property(x => x.PlanId).HasColumnType("int").IsRequired();
            builder.Property(x => x.TrainerId).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.UserId).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.ParentId).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.Review).HasColumnType("nvarchar(500)").IsRequired(false);
            builder.Property(x => x.Rate).HasColumnType("int").IsRequired(false);
            builder.Property(x => x.CommentStatus).HasColumnType("int").IsRequired();


            builder.HasOne<Plan>(x => x.Plan_Comment)
                .WithMany(z => z.Comments)
                .HasForeignKey(a => a.PlanId)
                .IsRequired();

            builder.HasOne<TrainerProfile>(x => x.TrainerProfile_Comment)
                .WithMany(z => z.Comments)
                .HasForeignKey(a => a.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(x => x.User_Comment)
                .WithMany(z => z.Comments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Comment>(x => x.Comment_Parent)
                .WithMany(z => z.Comments)
                .HasForeignKey(a => a.ParentId)
                .IsRequired(false);
        }
    }
}