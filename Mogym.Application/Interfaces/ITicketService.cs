using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Ticket;
using Mogym.Application.Records.TicketDetail;

namespace Mogym.Application.Interfaces
{
    public interface ITicketService
    {
        Task<int> CreateOrGetTickets(int planId);
        Task SendMessage(string message, int ticketCode);
        Task<List<MyTicketRecord>> MyTickets();
        Task<List<TicketDetailRecord>> ViewTicketDetail(int ticketCode);

        Task<int> GetMyUnreadTicketsCount();
    }
}
