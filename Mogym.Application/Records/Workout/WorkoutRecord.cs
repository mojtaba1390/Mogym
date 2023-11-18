using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Workout
{
    public record WorkoutRecord
    {
        public int? Id { get; init; }
        public string? Title { get; init; }
        public int? PlanId { get; init; }
    }
}
