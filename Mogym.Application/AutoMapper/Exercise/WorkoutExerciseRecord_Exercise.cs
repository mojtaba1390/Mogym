using Mogym.Application.Records.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.AutoMapper.Exercise
{
    public class WorkoutExerciseRecord_Exercise:global::AutoMapper.Profile
    {
        public WorkoutExerciseRecord_Exercise()
        {
            CreateMap<WorkoutExerciseRecord, Domain.Entities.Exercise>()
                .ForMember(x => x.RowVersion, z => z.Ignore());

        }
    }
}
