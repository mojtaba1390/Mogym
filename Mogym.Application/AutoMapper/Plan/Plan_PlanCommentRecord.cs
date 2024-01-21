using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;

namespace Mogym.Application.AutoMapper.Plan
{
    public class Plan_PlanCommentRecord:global::AutoMapper.Profile
    {
        public Plan_PlanCommentRecord()
        {
            CreateMap<Domain.Entities.Plan, PlanCommentRecord>()
                .ForMember(x => x.PlanId,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.TrainerFullName,
                    z => z.MapFrom(
                        a => a.TrainerProfile_Plan.User.FirstName + " " + a.TrainerProfile_Plan.User.LastName))
                .ForMember(x => x.Code,
                    z => z.MapFrom(a => a.TrackingCode));
        }
    }
}
