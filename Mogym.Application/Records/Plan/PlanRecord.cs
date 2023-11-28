using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.Records.Plan
{
    public record PlanRecord
    {
        public int? Id { get; init; }
        public string TrainerName { get; init; }
        public string? Description { get; init; }
        public EnumPlanStatus PlanStatus { get; init; }
        public DateTime InsertDate { get; init; }

    }

}
