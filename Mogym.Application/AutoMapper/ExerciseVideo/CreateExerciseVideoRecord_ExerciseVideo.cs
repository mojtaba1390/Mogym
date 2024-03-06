using Mogym.Application.Records.ExerciseVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.ExerciseVideo
{
    public class CreateExerciseVideoRecord_ExerciseVideo:global::AutoMapper.Profile
    {
        public CreateExerciseVideoRecord_ExerciseVideo()
        {
            CreateMap<CreateExerciseVideoRecord, Domain.Entities.ExerciseVideo>()
                .ForMember(x=>x.Title,
                    z=>z.MapFrom(a=>a.Title+"-اختصاصی"))
                .ForMember(x=>x.FileName,
                    z=>z.MapFrom(a=>a.ExerciseVideo.FileName))
                .ForMember(x => x.Status,
                    z => z.MapFrom(a => EnumExrciseVideoStatus.UnderConsideration));
        }
    }
}
