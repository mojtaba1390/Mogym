using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Ticket:BaseEntity
    {
        public Ticket()
        {
            TicketDetails = new HashSet<TicketDetail>();
        }
        public int PlanId { get; set; }
        public int CreatorId { get; set; }
        public int AssignId  { get; set; }

        public EnumTicketStatus CreatorStatus { get; set; }
        public EnumTicketStatus AssignStatus { get; set; }

        public DateTime? CreatorLastSeen { get; set; }
        public DateTime? AssignLastSeen { get; set; }

        
        public User User_Creator { get; set; }
        public User User_Assign { get; set; }

        public ICollection<TicketDetail> TicketDetails { get; set; }
    }
}
