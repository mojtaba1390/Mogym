using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;

namespace Mogym.Application.AutoMapper.Plan
{
    public class Plan_ApprovePlan:global::AutoMapper.Profile
    {
        public Plan_ApprovePlan()
        {
            CreateMap<Domain.Entities.Plan,ApprovePlanRecord>();
        }
    }
}
