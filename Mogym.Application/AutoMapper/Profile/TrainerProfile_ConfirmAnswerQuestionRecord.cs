using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Question;
using Mogym.Common;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper.Profile
{
    public class TrainerProfile_ConfirmAnswerQuestionRecord : global::AutoMapper.Profile
    {
        public TrainerProfile_ConfirmAnswerQuestionRecord()
        {
            CreateMap<TrainerProfile, ConfirmAnswerQuestionRecord>()
                .ForMember(x => x.PlanName,
                    z =>
                        z.MapFrom(a => a.TrainerPlanCosts.First().TrainerPlan.GetEnumDescription()))
                .ForMember(x => x.PlanCost,
                    z =>
                        z.MapFrom(a => a.TrainerPlanCosts.First().OriginalCost))
                .ForMember(x => x.TrainerName,
                    z =>
                        z.MapFrom(a => a.User.FirstName + " " + a.User.LastName))
                .ForMember(x => x.CartNumber,
                    z =>
                        z.MapFrom(a => a.CartNumber))
                .ForMember(x => x.CartOwner,
                    z =>
                        z.MapFrom(a =>
                        string.IsNullOrWhiteSpace(a.CartOwnerName)
                            ? a.CartOwnerName
                            : a.User.FirstName + " " + a.User.LastName));
        }
    }
}
