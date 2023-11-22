using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.ExerciseSet;

namespace Mogym.Application.AutoMapper.ExerciseSet
{
    public class ExerciseSet_ExerciseSetRecord:global::AutoMapper.Profile
    {
        public ExerciseSet_ExerciseSetRecord()
        {
            CreateMap<Domain.Entities.ExerciseSet, ExerciseSetRecord>().ReverseMap();
        }
    }
}
