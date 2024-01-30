using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Plan
{
    public record WaitForPayRecord
    {
        public int PlanId { get; init; }
        public int TrainingPlanId { get; init; }
        public string TrainerFullName { get; init; }
        public string TrainingPlanName { get; init; }
        public double TrainingPlanPrice { get; init; }
        public int? DiscountId { get; init; }
        public double LastPrice { get; init; }
    }
}
