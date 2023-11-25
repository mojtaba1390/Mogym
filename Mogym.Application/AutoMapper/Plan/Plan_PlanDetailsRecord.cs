using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;

namespace Mogym.Application.AutoMapper.Plan
{
    public class Plan_PlanDetailsRecord:global::AutoMapper.Profile
    {
        public Plan_PlanDetailsRecord()
        {
            CreateMap<Domain.Entities.Plan, PlanDetailsRecord>()
                .ForMember(x => x.PlanId, z => z.MapFrom(a => a.Id))
                .ForMember(x => x.WorkoutRecords, z => z.MapFrom(a => a.Workouts))
                .ForMember(x => x.MealRecords, z => z.MapFrom(a => a.Meals));
        }
    }
}
