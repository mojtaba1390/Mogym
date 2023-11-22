using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.ExerciseSet
{
    public record ExerciseSetRecord
    {
        public int? Id { get; init; }
        public int? Weight { get; init; }
        public int? Count { get; init; }
        public int? Minute { get; init; }
        public int? Second { get; init; }
        public int ExerciseId { get; init; }

    }
}
