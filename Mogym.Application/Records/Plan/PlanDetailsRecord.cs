using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Workout;

namespace Mogym.Application.Records.Plan
{
    public record PlanDetailsRecord
    {
        public int PlanId { get; init; }

        public List<WorkoutRecord> WorkoutRecords { get; init; }
    }
}
