using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Workout
{
    public record WorkoutExerciseRecord
    {
        public int? Id { get; init; }
        public int? WorkoutId { get; init; }
        public int? ExerciseVideoId { get; init; }
        public int? SuperSetId { get; init; }

    }
}
