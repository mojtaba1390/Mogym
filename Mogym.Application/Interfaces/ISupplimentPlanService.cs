using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Suppliment;
using Mogym.Application.Records.SupplimentPlan;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface ISupplimentPlanService
    {
        Task AddOrUpdate(List<SupplimentPlanRecord> supplimentPlanRecords);
        Task Delete(int deleteId);
        Task<List<SentSupplimentPlanRecord>> GetSentSupplimentDetail(int planId);
        Task Edit(int id, string title);
        Task<SupplimentPlan?> GetByIdAsync(int supplimentId);
    }
}
