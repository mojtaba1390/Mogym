using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.SupplimentPlanDetail;

namespace Mogym.Application.AutoMapper.SupplimentPlanDetail
{
    public class SupplimentPlanDetail_SupplimentPlanDetailRecord:global::AutoMapper.Profile
    {
        public SupplimentPlanDetail_SupplimentPlanDetailRecord()
        {
            CreateMap<Domain.Entities.SupplimentPlanDetail, SupplimentPlanDetailRecord>().ReverseMap();
        }
    }
}
