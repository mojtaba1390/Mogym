using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class UserProfile:BaseEntity
    {
        public UserProfile()
        {
            TrainerPlanCosts = new HashSet<TrainerPlanCost>();
            Achievements = new HashSet<Achievement>();
        }

        public string? Biography { get; set; }








        //relations
        public User User { get; set; }


        public ICollection<TrainerPlanCost> TrainerPlanCosts { get; set; }
        public ICollection<Achievement> Achievements { get; set; }


    }
}
