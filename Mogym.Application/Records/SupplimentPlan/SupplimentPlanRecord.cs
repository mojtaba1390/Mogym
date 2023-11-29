using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.SupplimentPlan
{
    public record SupplimentPlanRecord
    {
        public int? Id { get; init; }
        public int? PlanId { get; init; }
        public string Title { get; init; }
    }

}
