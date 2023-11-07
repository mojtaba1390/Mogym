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

        public int TrainerProfileId { get; set; }


        public TrainerProfile TrainerPlanCost_TrainerProfile { get; set; }
    }
}
