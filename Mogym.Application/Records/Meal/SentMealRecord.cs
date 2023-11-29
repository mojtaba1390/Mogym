using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.MealIngridient;

namespace Mogym.Application.Records.Meal
{
    public record SentMealRecord
    {
        public int Id { get; init; }
        public string Title { get; init; }

        public List<SentMealIngridientRecord> MealIngridients { get; init; }
    }
}
