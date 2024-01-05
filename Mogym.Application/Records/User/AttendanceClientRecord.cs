using Mogym.Application.Records.TrainerPlanCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.User
{
    public class AttendanceClientRecord
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Mobile { get; init; }

        public List<TrainerPlanCostRecord>? TrainerPlanCosts { get; init; }


        public int TrainerPlanId { get; init; }
    }
}
