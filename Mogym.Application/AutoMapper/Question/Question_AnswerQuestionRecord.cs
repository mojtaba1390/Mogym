using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Question;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.Question
{
    public class Question_AnswerQuestionRecord : global::AutoMapper.Profile
    {
        public Question_AnswerQuestionRecord()
        {
            CreateMap<Domain.Entities.Question, AnswerQuestionRecord>()
                .ForMember(x => x.Gender, z => z.MapFrom(a => ((EnumGender)a.Gender).GetEnumDescription()))
                .ForMember(x => x.DailyAvtivity,
                    z => z.MapFrom(a => a.DailyAvtivity != null ? ((EnumDailyAvtivity)a.DailyAvtivity).GetEnumDescription() : string.Empty))
                .ForMember(x => x.NightWork,
                    z => z.MapFrom(a => a.NightWork != null ? ((EnumYesNo)a.NightWork).GetEnumDescription() : string.Empty))
                .ForMember(x => x.HeartDisease,
                    z => z.MapFrom(a => a.HeartDisease != null ? ((EnumYesNo)a.HeartDisease).GetEnumDescription() : string.Empty))
                .ForMember(x => x.DiabetesAsthmaHypoglycemia,
                    z => z.MapFrom(a =>
                        a.DiabetesAsthmaHypoglycemia != null
                            ? ((EnumYesNo)a.DiabetesAsthmaHypoglycemia).GetEnumDescription()
                            : string.Empty))
                .ForMember(x => x.Smoke,
                    z => z.MapFrom(a => a.Smoke != null ? ((EnumYesNo)a.Smoke).GetEnumDescription() : string.Empty))
                .ForMember(x => x.SessionsInWeek,
                    z => z.MapFrom(a =>
                        a.SessionsInWeek != null ? ((EnumSessionsInWeek)a.SessionsInWeek).GetEnumDescription() : string.Empty))
                .ForMember(x => x.TrainerPlan,
                    z => z.MapFrom(a => a.TrainerPlan != null ? ((EnumTrainerPlan)a.TrainerPlan).GetEnumDescription() : string.Empty));
        }
    }
}
