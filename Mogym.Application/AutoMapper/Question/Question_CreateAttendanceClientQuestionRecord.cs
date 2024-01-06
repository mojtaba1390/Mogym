using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Question;

namespace Mogym.Application.AutoMapper.Question
{
    public class Question_CreateAttendanceClientQuestionRecord:global::AutoMapper.Profile
    {
        public Question_CreateAttendanceClientQuestionRecord()
        {
            CreateMap<Domain.Entities.Question, CreateAttendanceClientQuestionRecord>()
                .ForMember(x => x.FirstName,
                    z => z.MapFrom(a => a.FirstName))
                .ForMember(x => x.LastName,
                    z => z.MapFrom(a => a.LastName))
                .ForMember(x => x.Mobile,
                    z => z.MapFrom(a => a.Plans.FirstOrDefault().User_Plan.Mobile))
                .ForMember(x => x.TrainerPlanId,
                    z => z.MapFrom(a => a.TrainerPlan))
                .ForMember(x => x.TrainerPlanCosts,
                    z => z.MapFrom(a =>
                        a.Plans.FirstOrDefault().TrainerProfile_Plan.TrainerPlanCosts
                            .Where(q => (int) q.TrainerPlan > 3)))
                .ForMember(x => x.QuestionId,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.TrainerFullName,
                    z => z.MapFrom(a =>
                        a.Plans.FirstOrDefault().TrainerProfile_Plan.User.FirstName + " " +
                        a.Plans.FirstOrDefault().TrainerProfile_Plan.User.LastName))
                .ForMember(x => x.Code,
                    z => z.MapFrom(a => a.Code))
                .ForMember(x => x.TrainerId,
                    z => z.MapFrom(a => a.Plans.FirstOrDefault().TrainerProfile_Plan.Id))
                .ForMember(x => x.PlanId,
                    z => z.MapFrom(a => a.Plans.FirstOrDefault().Id));
        }
    }
}
