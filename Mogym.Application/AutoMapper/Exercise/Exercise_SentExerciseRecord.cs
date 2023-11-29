using Mogym.Application.Records.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Exercise;

namespace Mogym.Application.AutoMapper.Exercise
{
    public class Exercise_SentExerciseRecord:global::AutoMapper.Profile
    {
        public Exercise_SentExerciseRecord()
        {
            CreateMap<Domain.Entities.Exercise, SentExerciseRecord>()
                .ForMember(x => x.Id,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.WorkoutId,
                    z => z.MapFrom(a => a.WorkoutId))
                .ForMember(x => x.ExerciseId,
                    z => z.MapFrom(a => a.ExerciseVideoId))
                .ForMember(x => x.ExerciseTitle,
                    z => z.MapFrom(
                        a => a.ExerciseVideo_Exercise != null ? a.ExerciseVideo_Exercise.Title : String.Empty))
                .ForMember(x => x.SuperSetId,
                    z => z.MapFrom(a => a.SuperSetId))
                .ForMember(x => x.ExerciseVideoId,
                    z => z.MapFrom(a =>
                        a.ExerciseVideoId))
                .ForMember(x=>x.SentExerciseSetRecords,
                    z=>z.MapFrom(a=>a.ExerciseSets));

        }
    }
}
