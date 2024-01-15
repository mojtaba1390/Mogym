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
    public class TicketDetailConfiguration:IEntityTypeConfiguration<TicketDetail>
    {
        public void Configure(EntityTypeBuilder<TicketDetail> builder)
        {
            builder.ToTable("TicketDetail");
            builder.Property(x => x.TicketId).HasColumnType("int").IsRequired();
            builder.Property(x => x.UserId).HasColumnType("int").IsRequired();
            builder.Property(x => x.Message).HasColumnType("nvarchar(1000)").IsRequired();


            builder.HasOne<User>(x => x.User_TiketDetail)
                .WithMany(z => z.TicketDetails)
                .HasForeignKey(a => a.UserId)
                .IsRequired();

            builder.HasOne<Ticket>(x => x.Ticket_TicketDetail)
                .WithMany(z => z.TicketDetails)
                .HasForeignKey(a => a.TicketId)
                .IsRequired();
        }
    }
}
