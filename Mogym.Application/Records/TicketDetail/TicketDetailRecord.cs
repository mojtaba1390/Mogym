using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.TicketDetail
{
    public record TicketDetailRecord
    {
        public int UserId { get; init; }
        public string FullName { get; init; }
        public string Message { get; init; }
        public string InsertDate { get; init; }
        
    }
}
