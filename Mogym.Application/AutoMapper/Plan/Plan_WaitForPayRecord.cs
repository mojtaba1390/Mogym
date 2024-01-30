using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.Plan
{
    public class Plan_WaitForPayRecord:global::AutoMapper.Profile
    {
        public Plan_WaitForPayRecord()
        {
            CreateMap<Domain.Entities.Plan, WaitForPayRecord>()
                .ForMember(x => x.PlanId,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.TrainingPlanId,
                    z => z.MapFrom(a => a.TrainerProfile_Plan.TrainerPlanCosts.FirstOrDefault().Id))
                .ForMember(x => x.TrainerFullName,
                    z => z.MapFrom(
                        a => a.TrainerProfile_Plan.User.FirstName + " " + a.TrainerProfile_Plan.User.LastName))
                .ForMember(x => x.TrainingPlanName,
                    z => z.MapFrom(a =>
                        a.TrainerProfile_Plan.TrainerPlanCosts.FirstOrDefault().TrainerPlan.GetEnumDescription()))
                .ForMember(x => x.TrainingPlanPrice,
                    z => z.MapFrom(a =>
                        a.TrainerProfile_Plan.TrainerPlanCosts.FirstOrDefault().OriginalCost))
                .ForMember(x => x.LastPrice,
                    z => z.MapFrom(a =>
                        a.TrainerProfile_Plan.TrainerPlanCosts.FirstOrDefault().OriginalCost));
        }
    }
}
