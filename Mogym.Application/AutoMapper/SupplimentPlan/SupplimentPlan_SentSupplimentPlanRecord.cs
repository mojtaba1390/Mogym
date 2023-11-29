using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.SupplimentPlan;

namespace Mogym.Application.AutoMapper.SupplimentPlan
{
    public class SupplimentPlan_SentSupplimentPlanRecord:global::AutoMapper.Profile
    {
        public SupplimentPlan_SentSupplimentPlanRecord()
        {
            CreateMap<Domain.Entities.SupplimentPlan, SentSupplimentPlanRecord>()
                .ForMember(x => x.SentSupplimentRecords,
                    z => z.MapFrom(a => a.SupplimentPlanDetails));
        }
    }
}
