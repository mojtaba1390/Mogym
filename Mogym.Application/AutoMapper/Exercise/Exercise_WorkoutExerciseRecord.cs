using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Workout;

namespace Mogym.Application.AutoMapper.Exercise
{
    public class Exercise_WorkoutExerciseRecord:global::AutoMapper.Profile
    {
        public Exercise_WorkoutExerciseRecord()
        {
            CreateMap<Domain.Entities.Exercise, WorkoutExerciseRecord>().ReverseMap();
        }
    }
}
