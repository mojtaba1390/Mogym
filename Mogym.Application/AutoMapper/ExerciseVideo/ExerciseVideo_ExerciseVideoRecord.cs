using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Exercise;

namespace Mogym.Application.AutoMapper.ExerciseVideo
{
    public class ExerciseVideo_ExerciseVideoRecord:global::AutoMapper.Profile
    {
        public ExerciseVideo_ExerciseVideoRecord()
        {
            CreateMap<Domain.Entities.ExerciseVideo, ExerciseRecord>().ReverseMap();
        }
    }
}
