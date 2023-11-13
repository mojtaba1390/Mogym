using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.TrainerPlanCost
{
    public record CreateTrainerCostsRecord
    {
        public int? TrainerPlan { get; init; }
        public double OriginalCost { get; init; }
    }
}
