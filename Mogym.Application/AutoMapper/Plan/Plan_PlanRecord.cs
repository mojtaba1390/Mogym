using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;

namespace Mogym.Application.AutoMapper.Plan
{
    public class Plan_PlanRecord:global::AutoMapper.Profile
    {
        public Plan_PlanRecord()
        {
            CreateMap<Domain.Entities.Plan, PlanRecord>()
                .ForMember(x => x.TrainerName,
                    z => z.MapFrom(
                        a => a.TrainerProfile_Plan.User.FirstName + " " + a.TrainerProfile_Plan.User.LastName));
        }
    }
}
