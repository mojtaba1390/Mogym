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
                .ForMember(x => x.Gender, z => z.MapFrom(a => a.Gender.GetEnumDescription()))
                .ForMember(x => x.DailyAvtivity,
                    z => z.MapFrom(a => a.DailyAvtivity != 0 ? a.DailyAvtivity.GetEnumDescription() : string.Empty))
                .ForMember(x => x.NightWork,
                    z => z.MapFrom(a => a.NightWork != 0 ? a.NightWork.GetEnumDescription() : string.Empty))
                .ForMember(x => x.HeartDisease,
                    z => z.MapFrom(a => a.HeartDisease != 0 ? a.HeartDisease.GetEnumDescription() : string.Empty))
                .ForMember(x => x.DiabetesAsthmaHypoglycemia,
                    z => z.MapFrom(a =>
                        a.DiabetesAsthmaHypoglycemia != 0
                            ? a.DiabetesAsthmaHypoglycemia.GetEnumDescription()
                            : string.Empty))
                .ForMember(x => x.Smoke,
                    z => z.MapFrom(a => a.Smoke != 0 ? a.Smoke.GetEnumDescription() : string.Empty))
                .ForMember(x => x.SessionsInWeek,
                    z => z.MapFrom(a =>
                        a.SessionsInWeek != 0 ? a.SessionsInWeek.GetEnumDescription() : string.Empty))
                .ForMember(x => x.Target,
                    z => z.MapFrom(a => a.Target != 0 ? a.Target.GetEnumDescription() : String.Empty));
        }
    }
}
