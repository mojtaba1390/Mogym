using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.ExerciseSet;

namespace Mogym.Application.AutoMapper.ExerciseSet
{
    public class ExerciseSetRecord_ExerciseSet:global::AutoMapper.Profile
    {
        public ExerciseSetRecord_ExerciseSet()
        {
            CreateMap<ExerciseSetRecord, Domain.Entities.ExerciseSet>()
                .ForMember(x => x.RowVersion, z => z.Ignore());
        }
    }
}
