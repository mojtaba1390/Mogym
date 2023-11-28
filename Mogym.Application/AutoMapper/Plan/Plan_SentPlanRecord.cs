using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;

namespace Mogym.Application.AutoMapper.Plan
{
    public class Plan_SentPlanRecord:global::AutoMapper.Profile
    {
        public Plan_SentPlanRecord()
        {
            CreateMap<Domain.Entities.Plan, SentPlanRecord>();
        }
    }
}
