using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Meal:BaseEntity
    {
        public Meal()
        {
            MealIngridients = new HashSet<MealIngridient>();
        }
        public int PlanId { get; set; }

        public string Title { get; set; }





        public Plan Plan_Meal { get; set; }

        public ICollection<MealIngridient> MealIngridients { get; set; }
    }
}
