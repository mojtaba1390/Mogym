using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Suppliment;

namespace Mogym.Application.Records.SupplimentPlan
{
    public record SentSupplimentPlanRecord: SupplimentPlanRecord
    {
        public List<SentSupplimentRecord> SentSupplimentRecords { get; init; }
    }
}
