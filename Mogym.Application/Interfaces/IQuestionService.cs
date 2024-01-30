using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Question;

namespace Mogym.Application.Interfaces
{
    public interface IQuestionService
    {
        Task<WaitForPayRecord> AddQuestion(CreateQuestionRecord createQuestionRecord);
        Task<CreateAttendanceClientQuestionRecord> GetQuestionWithCode(string code);
        Task<WaitForPayRecord> UpdateQuestion(CreateAttendanceClientQuestionRecord createAttendanceClientQuestionRecord);
    }
}
