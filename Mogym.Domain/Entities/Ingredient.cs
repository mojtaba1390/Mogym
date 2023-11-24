using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Ingredient:BaseEntity
    {
        public Ingredient()
        {
            MealIngridients = new HashSet<MealIngridient>();
        }

        public string Title { get; set; }

        public ICollection<MealIngridient> MealIngridients { get; set; }
    }
}
