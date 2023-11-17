using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Plan
{
    public record PaidPlanRecorrd: PlanRecord
    {
        public string AthletName { get; init; }
        public string PaidPicture { get; set; }
    }
}
