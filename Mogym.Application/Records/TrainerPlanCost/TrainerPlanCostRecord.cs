using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.TrainerPlanCost
{
    public record TrainerPlanCostRecord
    {

        public int? Id { get; init; }
        public string? TrainerPlan { get; init; }
        public double? Cost { get; init; }
    }
}
