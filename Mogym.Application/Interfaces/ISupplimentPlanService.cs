using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.AutoMapper.SupplimentPlan;

namespace Mogym.Application.Interfaces
{
    public interface ISupplimentPlanService
    {
        void AddOrUpdate(List<SupplimentPlanRecord> supplimentPlanRecords);
    }
}
