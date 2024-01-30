using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Profile;

namespace Mogym.Application.Interfaces
{
    public interface IFinanceService
    {
        Task<TrainerPaymentRecord> ApproveForPay(WaitForPayRecord model);
    }
}
