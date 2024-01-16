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
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.Property(x => x.PlanId).HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatorId).HasColumnType("int").IsRequired();
            builder.Property(x => x.AssignId).HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatorStatus).HasColumnType("int").IsRequired();
            builder.Property(x => x.AssignStatus).HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatorLastSeen).HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.AssignLastSeen).HasColumnType("datetime2").IsRequired(false);
            builder.Property(x => x.TicketCode).HasColumnType("int").IsRequired();
            builder.Property(x => x.IsSentSmsToCreator).HasColumnType("int").IsRequired();
            builder.Property(x => x.IsSentSmsToAssign).HasColumnType("int").IsRequired();


            builder.HasOne<User>(x => x.User_Creator)
                .WithMany(z => z.Creators)
                .HasForeignKey(a => a.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne<User>(x => x.User_Assign)
                .WithMany(z => z.Assigns)
                .HasForeignKey(a => a.AssignId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
