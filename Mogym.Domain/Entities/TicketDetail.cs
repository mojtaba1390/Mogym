using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class TicketDetail:BaseEntity
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }

        public string Message { get; set; }

        public Ticket Ticket_TicketDetail { get; set; }
        public User User_TiketDetail { get; set; }
    }
}
