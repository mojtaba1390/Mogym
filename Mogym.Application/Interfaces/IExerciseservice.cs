using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Workout;

namespace Mogym.Application.Interfaces
{
    public interface IExerciseservice
    {
        Task AddAndUpdateExercises(List<WorkoutExerciseRecord> workoutExerciseRecords);
        Task Delete(int id);
        Task<bool> IsAnyExcerciseExistByWorkoutId(int id);
    }
}
