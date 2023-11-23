using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.ExerciseSet;

namespace Mogym.Application.Interfaces
{
    public interface IExerciseSetService
    {
        Task<List<ExerciseSetRecord>> GetExerciseSetDetail(int exerciseId);
        void AddOrUpdateSets(List<ExerciseSetRecord> exerciseSetRecords);
    }
}
