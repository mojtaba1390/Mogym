using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Exercise;

namespace Mogym.Application.Records.Workout
{
    public record SentWorkoutRecord
    {
       public int Id { get; init; }
        public int PlanId { get; init; }
        public string Title { get; init; }
        public  List<SentExerciseRecord> SentExerciseRecords { get; init; }
    }
    

}
