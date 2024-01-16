using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Ticket;

namespace Mogym.Application.AutoMapper.Ticket
{
    public class Ticket_MyTicketRecord:global::AutoMapper.Profile
    {
        public Ticket_MyTicketRecord()
        {
            CreateMap<Domain.Entities.Ticket, MyTicketRecord>();
        }
    }
}
