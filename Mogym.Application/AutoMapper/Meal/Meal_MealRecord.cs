using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Meal;

namespace Mogym.Application.AutoMapper.Meal
{
    public class Meal_MealRecord:global::AutoMapper.Profile
    {
        public Meal_MealRecord()
        {
            CreateMap<Domain.Entities.Meal, MealRecord>().ReverseMap();
        }
    }
}
