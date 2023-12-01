﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Suppliment;
using Mogym.Application.Records.SupplimentPlan;

namespace Mogym.Application.Interfaces
{
    public interface ISupplimentPlanService
    {
        Task AddOrUpdate(List<SupplimentPlanRecord> supplimentPlanRecords);
        Task Delete(int deleteId);
        Task<List<SentSupplimentPlanRecord>> GetSentSupplimentDetail(int planId);
    }
}