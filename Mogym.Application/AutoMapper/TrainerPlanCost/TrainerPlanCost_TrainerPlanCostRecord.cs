using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TrainerPlanCost;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.TrainerPlanCost
{
    public class TrainerPlanCost_TrainerPlanCostRecord:global::AutoMapper.Profile
    {
        public TrainerPlanCost_TrainerPlanCostRecord()
        {
            CreateMap<Domain.Entities.TrainerPlanCost, TrainerPlanCostRecord>()
                .ForMember(x => x.TrainerPlan, z => z.MapFrom(a => a.TrainerPlan.GetEnumDescription()))
                .ForMember(x => x.Cost, z => z.MapFrom(a => a.OriginalCost));
        }
    }
}
