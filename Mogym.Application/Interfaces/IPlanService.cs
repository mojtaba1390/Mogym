using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Question;
using Mogym.Application.Records.User;
using Mogym.Application.Records.Workout;
using Mogym.Common;

namespace Mogym.Application.Interfaces
{
    public interface IPlanService
    {
        Task<List<PlanRecord>?> MyUnPaidPlans();
        Task UpdatePaidPicture(int planId,string paidPictureFileName);
        Task<bool> IsThisPlanIdForThisCurrentUser(int planId);
        Task<AnswerQuestionRecord> GetAnswerQuestionWithPlanId(int planId);
        Task<List<PaidPlanRecorrd>?> GetPaidPlans();
        Task ApprovePlan(int planId);
        Task<List<ApprovePlanRecord>?> GetApprovePlans();
        Task<PlanDetailsRecord> GetPlanDetails(int planId);
        Task AddDescription(int planId, string description);
        Task SendPlan(int planId);
        Task<List<SentPlanRecord>?> GetSentPlans();
        Task<List<PlanRecord>> GetMyPaidPlans();
        Task<List<PlanRecord>> MyApprovedPlans();
        Task<List<PlanRecord>> MyRecivedPlans();
        Task<bool> IsThisPlanIdForThisTrainer(int planId);
        Task<bool> IsThereAnyPlanWithStatus(int planId, EnumPlanStatus planStatus);
        Task<List<PaidPlanRecorrd>?> CheckPaidPic();
        Task ApprovePic(int planId);
        Task IgnorePic(int planId);
        Task AddAttendancClientRequest(AttendanceClientRecord attendanceClientRecord);

        //TODO:مدل خروجی باید عوض بشه و بر اساس اسم متد باشه
        Task<List<PaidPlanRecorrd>> GetAttendanceClientRequests();
    }
}
