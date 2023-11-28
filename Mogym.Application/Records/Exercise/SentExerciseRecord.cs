using Mogym.Application.Records.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.ExerciseSet;

namespace Mogym.Application.Records.Exercise
{
    public record SentExerciseRecord
    {
        public int Id { get; init; }
        public int WorkoutId { get; init; }
        public string ExerciseTitle { get; init; }
        public int ExerciseId { get; init; }
        public string? ExerciseFileName { get; init; }
        public int? SuperSetId { get; init; }
        public List<SentExerciseSetRecord> SentExerciseSetRecords{ get; init; }
    }

}
