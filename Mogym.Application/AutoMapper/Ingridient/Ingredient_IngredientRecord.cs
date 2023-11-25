using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Ingridient;

namespace Mogym.Application.AutoMapper.Ingridient
{
    public class Ingredient_IngredientRecord:global::AutoMapper.Profile
    {
        public Ingredient_IngredientRecord()
        {
            CreateMap<Domain.Entities.Ingredient, IngredientRecord>().ReverseMap();
        }
    }
}
