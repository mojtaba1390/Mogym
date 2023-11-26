using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class SupplimentPlanDetail:BaseEntity
    {
        public int SupplimentPlanId { get; set; }

        public int SupplimentId { get; set; }

        public double Amount { get; set; }
        public EnumScale Scale { get; set; }


        public Suppliment Suppliment_SupplimentPlanDetail { get; set; }
        public SupplimentPlan SupplimentPlan_SupplimentPlanDetail { get; set;}

    }
}
