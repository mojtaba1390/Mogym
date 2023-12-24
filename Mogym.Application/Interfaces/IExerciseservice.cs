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
        Task<string> AddAndUpdateExercises(List<WorkoutExerciseRecord> workoutExerciseRecords);
         Task<string> Delete(int id, int workoutId);
        Task<bool> IsAnyExcerciseExistByWorkoutId(int id);
        Task<List<int?>> GetWorkoutSuperSetIds(int workoutId);
    }
}
