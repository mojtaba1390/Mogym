using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.SupplimentPlan;

namespace Mogym.Application.AutoMapper.SupplimentPlan
{
    public class SupplimentPlan_SupplimentPlanRecord:global::AutoMapper.Profile
    {
        public SupplimentPlan_SupplimentPlanRecord()
        {
            CreateMap<Domain.Entities.SupplimentPlan, SupplimentPlanRecord>().ReverseMap();
        }
    }
}
