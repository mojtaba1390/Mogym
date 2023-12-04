using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Question
{
    public record ConfirmAnswerQuestionRecord
    {
        public string PlanName { get; init; }
        public double PlanCost { get; init; }
        public string TrainerName { get; init; }
        public string CartNumber { get; init; }
        public string CartOwner { get; init; }
    }
}
