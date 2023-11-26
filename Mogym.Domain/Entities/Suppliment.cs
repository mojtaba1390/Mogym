using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Suppliment:BaseEntity
    {
        public Suppliment()
        {
            SupplimentPlanDetails = new HashSet<SupplimentPlanDetail>();
        }
        public string Title { get; set; }




        public ICollection<SupplimentPlanDetail> SupplimentPlanDetails { get; set; }
    }
}
