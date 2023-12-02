using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Question;
using Mogym.Application.Records.Workout;

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
    }
}
