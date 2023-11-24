using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class MealIngridient:BaseEntity
    {
        public double? Count { get; set; }
        public EnumSize? Size { get; set; }
        public double? Amount { get; set; }



        public int  MealId { get; set; }
        public int  IngridientId { get; set; }

        public Meal Meal_MealIngridient { get; set; }
        public Ingredient Ingredient_MealIngridient { get; set; }
    }
}
