using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.SupplimentPlanDetail;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface ISupplimentPlanDetailService
    {
        Task<List<SupplimentPlanDetailRecord>> GetSupplimentPlanDetail(int supplimentPlanId);
        void AddOrUpdateSets(List<SupplimentPlanDetailRecord> supplimentPlanDetailRecords);
    }
}
