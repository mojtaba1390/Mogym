using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.AutoMapper.SupplimentPlan
{
    public record SupplimentPlanRecord(
        int? Id,
        int? SupplimentId,
        double? Amount,
        int? Scale,
        int? PlanId
        );

}
