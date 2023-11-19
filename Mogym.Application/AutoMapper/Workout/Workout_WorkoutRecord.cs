using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Workout;

namespace Mogym.Application.AutoMapper.Workout
{
    public class Workout_WorkoutRecord:global::AutoMapper.Profile
    {
        public Workout_WorkoutRecord()
        {
            CreateMap<Domain.Entities.Workout, WorkoutRecord>().ReverseMap();
        }
    }
}
