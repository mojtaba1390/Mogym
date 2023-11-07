using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class TrainerPlanCost:BaseEntity
    {
        public EnumTrainerPlan TrainerPlan { get; set; }
        public double? OriginalCost { get; set; }
        public double? SaleCost { get; set; }

        public int UserProfileId { get; set; }


        public UserProfile TrainerPlanCost_UserProfile { get; set; }
    }
}
