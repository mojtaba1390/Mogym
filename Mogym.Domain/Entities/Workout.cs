using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Workout:BaseEntity
    {
        public Workout()
        {
            Exercises = new HashSet<Exercise>();
        }
        public int PlanId { get; set; }

        public string Title { get; set; }





        public Plan Plan_Workout { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}
