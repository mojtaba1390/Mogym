using Mogym.Application.Records.ExerciseVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.AutoMapper.ExerciseVideo
{
    public class CreateExerciseVideoRecord_ExerciseVideo:global::AutoMapper.Profile
    {
        public CreateExerciseVideoRecord_ExerciseVideo()
        {
            CreateMap<CreateExerciseVideoRecord, Domain.Entities.ExerciseVideo>()
                .ForMember(x => x.FileName, z => z.MapFrom(a => a.ExerciseVideo.FileName));
        }
    }
}
