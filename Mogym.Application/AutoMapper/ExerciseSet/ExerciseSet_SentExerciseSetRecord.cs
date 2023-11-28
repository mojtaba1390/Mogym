using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.ExerciseSet;
using Mogym.Application.Records.Workout;

namespace Mogym.Application.AutoMapper.ExerciseSet
{
    public class ExerciseSet_SentExerciseSetRecord:global::AutoMapper.Profile
    {
        public ExerciseSet_SentExerciseSetRecord()
        {
            CreateMap<Domain.Entities.ExerciseSet, SentExerciseSetRecord>()
                .ForMember(x => x.Id,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.ExerciseId,
                    z => z.MapFrom(a => a.ExerciseId))
                .ForMember(x => x.Weight,
                    z => z.MapFrom(a => a.Weight))
                .ForMember(x => x.Count,
                    z => z.MapFrom(a => a.Count))
                .ForMember(x => x.Minute,
                    z => z.MapFrom(a => a.Minute))
                .ForMember(x => x.Second,
                    z => z.MapFrom(a => a.Second));
        }
    }
}
