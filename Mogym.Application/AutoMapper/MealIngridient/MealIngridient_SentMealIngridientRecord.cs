using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.MealIngridient;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.MealIngridient
{
    public class MealIngridient_SentMealIngridientRecord:global::AutoMapper.Profile
    {
        public MealIngridient_SentMealIngridientRecord()
        {
            CreateMap<Domain.Entities.MealIngridient, SentMealIngridientRecord>()
                .ForMember(x=>x.Id,
                    z=>z.MapFrom(a=>a.Id))
                .ForMember(x=>x.Count,
                    z=>z.MapFrom(a=>a.Count))
                .ForMember(x=>x.Amount,
                    z=>z.MapFrom(a=>a.Amount))
                .ForMember(x=>x.IngridientId,
                    z=>z.MapFrom(a=>a.Ingredient_MealIngridient.Id))
                .ForMember(x => x.IngridientTitle,
                    z => z.MapFrom(a => a.Ingredient_MealIngridient.Title))
                .ForMember(x => x.Size,
                    z => z.MapFrom(a => (int)a.Size != -1 ? ((EnumSize)a.Size).GetEnumDescription() : String.Empty));


        }
    }
}
