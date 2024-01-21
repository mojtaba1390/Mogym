using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Plan
{
    public record PlanCommentRecord
    {
        public int PlanId { get; init; }
        public int Code { get; init; }
        public string TrainerFullName { get; init; }
    }
}
