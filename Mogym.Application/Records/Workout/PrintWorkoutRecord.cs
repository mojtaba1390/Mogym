using Mogym.Application.Records.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Workout
{
    public record PrintWorkoutRecord: SentExerciseRecord
    {
        public string Title { get; init; }
    }
}
