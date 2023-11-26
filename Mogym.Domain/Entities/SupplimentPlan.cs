using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class SupplimentPlan:BaseEntity
    {
        public SupplimentPlan()
        {
            SupplimentPlanDetails = new HashSet<SupplimentPlanDetail>();
        }
        public string Description { get; set; }

        public int PlanId { get; set; }

        public Plan Plan_SupplimentPlan { get; set; }


        public ICollection<SupplimentPlanDetail> SupplimentPlanDetails { get; set;}
    }
}
