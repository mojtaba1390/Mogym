using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;

namespace Mogym.Application.Interfaces
{
    public interface IPlanService
    {
        Task<List<PlanRecord>?> GetMyPlans();
        Task UpdatePaidPicture(int planId,string paidPictureFileName);
    }
}
