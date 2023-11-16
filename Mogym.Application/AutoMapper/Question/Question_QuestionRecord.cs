using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Question;

namespace Mogym.Application.AutoMapper.Question
{
    public class Question_QuestionRecord:global::AutoMapper.Profile
    {
        public Question_QuestionRecord()
        {
            CreateMap<Domain.Entities.Question, QuestionRecord>();
        }
    }
}
