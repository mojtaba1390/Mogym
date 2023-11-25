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
        public int SupplimentId { get; set; }

        public double Amount { get; set; }
        public EnumScale Scale { get; set; }

        public int PlanId { get; set; }

        public Suppliment Suppliment_SupplimentPlan { get; set; }
        public Plan Plan_SupplimentPlan { get; set; }
    }
}
