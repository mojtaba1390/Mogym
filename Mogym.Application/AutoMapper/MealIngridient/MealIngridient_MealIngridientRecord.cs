using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.MealIngridient;

namespace Mogym.Application.AutoMapper.MealIngridient
{
    public class MealIngridient_MealIngridientRecord:global::AutoMapper.Profile
    {
        public MealIngridient_MealIngridientRecord()
        {
            CreateMap<Domain.Entities.MealIngridient, MealIngridientRecord>()
                .ForMember(x => x.Size,
                    z => z.MapFrom(a => a.Size!=null?a.Size:0)).ReverseMap();
        }
    }
}
