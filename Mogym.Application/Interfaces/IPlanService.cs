﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Question;

namespace Mogym.Application.Interfaces
{
    public interface IPlanService
    {
        Task<List<PlanRecord>?> GetMyPlans();
        Task UpdatePaidPicture(int planId,string paidPictureFileName);
        Task<bool> IsThisPlanIdForThisCurrentUser(int planId);
        Task<AnswerQuestionRecord> GetAnswerQuestionWithPlanId(int planId);
        Task<List<PaidPlanRecorrd>?> GetPaidPlans();
        Task ApprovePlan(int planId);
        Task<List<ApprovePlanRecord>?> GetApprovePlans();
        Task<PlanDetailsRecord> GetPlanDetails(int planId);
        Task AddDescription(int planId, string description);
    }
}
