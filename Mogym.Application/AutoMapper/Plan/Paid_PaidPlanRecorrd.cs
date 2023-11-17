using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;

namespace Mogym.Application.AutoMapper.Plan
{
    public class Paid_PaidPlanRecorrd:global::AutoMapper.Profile
    {
        public Paid_PaidPlanRecorrd()
        {
            CreateMap<Domain.Entities.Plan, PaidPlanRecorrd>()
                .ForMember(x => x.AthletName, z => z.MapFrom(a => a.User_Plan.FirstName + " " + a.User_Plan.LastName))
                .ForMember(x => x.PaidPicture, z => z.MapFrom(a => a.PaidPicture));
        }
    }
}
