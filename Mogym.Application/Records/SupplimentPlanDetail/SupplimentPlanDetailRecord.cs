using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.SupplimentPlanDetail
{
    public record SupplimentPlanDetailRecord
        (
            int? Id,
            int? Scale,
            double? Amount,
            int? SupplimentPlanId,
            int? SupplimentId
            );

}
