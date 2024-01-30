using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class DiscountUse:BaseEntity
    {
        public int UserId { get; set; }
        public int DiscountId { get; set; }
        public DateTime UseDate { get; set; }
    }
}
