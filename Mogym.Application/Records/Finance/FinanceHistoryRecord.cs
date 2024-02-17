using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Finance
{
    public record FinanceHistoryRecord
    {
        public string AthleteFullName { get; init; }
        public string TrainingPlanName { get; init; }
        public string InsertDate { get; set; }
        public string TrainingPlanActualPrice { get; init; }
        public string TrainingPlanPayPrice { get; init; }
        public string DiscountCode { get; init; }
        public string DiscountValue { get; init; }
        public string PayStatus { get; set; }
    }
}
