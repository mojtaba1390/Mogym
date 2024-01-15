using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TicketDetail;

namespace Mogym.Application.Interfaces
{
    public interface ITicketService
    {
        Task<Tuple<int,List<TicketDetailRecord>>> CreateOrGetTickets(int planId);
        Task SendMessage(string message, int ticketId);
    }
}
