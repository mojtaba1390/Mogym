using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.ExerciseVideo;

namespace Mogym.Application.Interfaces
{
    public interface IExerciseVideoService
    {
        Task<List<ExerciseVideoRecord>> GetAllExerciseVideo();
        Task AddAsync(CreateExerciseVideoRecord createExerciseVideoRecord);
    }
}
