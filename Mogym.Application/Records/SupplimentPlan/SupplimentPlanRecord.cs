using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.SupplimentPlan
{
    public record SupplimentPlanRecord
        (
            int? Id,
            int? PlanId,
            string Title
            );

}
