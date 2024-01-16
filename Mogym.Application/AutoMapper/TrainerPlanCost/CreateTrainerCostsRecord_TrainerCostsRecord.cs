using Mogym.Application.Records.TrainerPlanCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.TrainerPlanCost
{
    public class CreateTrainerCostsRecord_TrainerCostsRecord:global::AutoMapper.Profile
    {
        public CreateTrainerCostsRecord_TrainerCostsRecord()
        {
            CreateMap<CreateTrainerCostsRecord, Domain.Entities.TrainerPlanCost>()
                .ForMember(x => x.OriginalCost, z => z.MapFrom(a => a.OriginalCost))
                .ForMember(x=>x.IsDeleted,
                    z=>z.MapFrom(a=>EnumYesNo.No))
                .ForMember(x => x.TrainerPlan, z => z.MapFrom(a => (EnumTrainerPlan)a.TrainerPlan));
        }
    }
}
