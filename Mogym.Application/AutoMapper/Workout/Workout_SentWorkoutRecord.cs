using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Workout;

namespace Mogym.Application.AutoMapper.Workout
{
    public class Workout_SentWorkoutRecord:global::AutoMapper.Profile
    {
        public Workout_SentWorkoutRecord()
        {
            CreateMap<Domain.Entities.Workout, SentWorkoutRecord>()
                .ForMember(x => x.Id,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.PlanId,
                    z => z.MapFrom(a => a.PlanId))
                .ForMember(x => x.PlanId,
                    z => z.MapFrom(a => a.PlanId))
                .ForMember(x => x.SentExerciseRecords,
                    z => z.MapFrom(a => a.Exercises));

        }
    }
}
