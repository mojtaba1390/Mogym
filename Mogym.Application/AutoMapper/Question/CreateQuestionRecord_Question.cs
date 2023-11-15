using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Question;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.Question
{
    public class CreateQuestionRecord_Question:global::AutoMapper.Profile
    {
        public CreateQuestionRecord_Question()
        {
            CreateMap<CreateQuestionRecord, Domain.Entities.Question>()
                .ForMember(x => x.Gender, z => z.MapFrom(a => (EnumGender)a.Gender))
                .ForMember(x => x.DailyAvtivity,
                    z => z.MapFrom(a => a.DailyAvtivity != null ? (EnumDailyAvtivity)a.DailyAvtivity : 0))
                .ForMember(x => x.NightWork, z => z.MapFrom(a => a.NightWork != null ? (EnumYesNo)a.NightWork : 0))
                .ForMember(x => x.HeartDisease,
                    z => z.MapFrom(a => a.HeartDisease != null ? (EnumYesNo)a.HeartDisease : 0))
                .ForMember(x => x.DiabetesAsthmaHypoglycemia,
                    z => z.MapFrom(a =>
                        a.DiabetesAsthmaHypoglycemia != null ? (EnumYesNo)a.DiabetesAsthmaHypoglycemia : 0))
                .ForMember(x => x.Smoke, z => z.MapFrom(a => a.Smoke != null ? (EnumYesNo)a.Smoke : 0))
                .ForMember(x => x.SessionsInWeek,
                    z => z.MapFrom(a => a.SessionsInWeek != null ? (EnumSessionsInWeek)a.SessionsInWeek : 0))
                .ForMember(x => x.TrainerPlan, z => z.MapFrom(a => (EnumTrainerPlan)a.TrainerPlanId));

        }
    }
}
