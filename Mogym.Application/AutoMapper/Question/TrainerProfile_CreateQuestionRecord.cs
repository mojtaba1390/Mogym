using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Question;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper.Question
{
    public class TrainerProfile_CreateQuestionRecord:global::AutoMapper.Profile
    {
        public TrainerProfile_CreateQuestionRecord()
        {
            CreateMap<TrainerProfile, CreateQuestionRecord>()
                .ForMember(x => x.TrainerFullName, z => z.MapFrom(a => a.User.FirstName + " " + a.User.LastName))
                .ForMember(x => x.TrainerId, z => z.MapFrom(a => a.Id))
                .ForMember(x => x.TrainerPlanCosts, z => z.MapFrom(a => a.TrainerPlanCosts.Where(x=>(int)x.TrainerPlan<4)));
        }
    }
}
