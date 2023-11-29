using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Suppliment;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.SupplimentPlanDetail
{
    public class SupplimentPlanDetail_SentSupplimentRecord:global::AutoMapper.Profile
    {
        public SupplimentPlanDetail_SentSupplimentRecord()
        {
            CreateMap<Domain.Entities.SupplimentPlanDetail, SentSupplimentRecord>()
                .ForMember(x => x.Id,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.SupplimentId,
                    z => z.MapFrom(a => a.Suppliment_SupplimentPlanDetail.Id))
                .ForMember(x => x.Amount,
                    z => z.MapFrom(a => a.Amount))
                .ForMember(x => x.SupplimentTitle,
                    z => z.MapFrom(a =>
                        a.Suppliment_SupplimentPlanDetail.Title))
                .ForMember(x => x.Scale,
                    z => z.MapFrom(a =>
                        (int)a.Scale != -1 ? ((EnumScale)a.Scale).GetEnumDescription() : String.Empty));
        }
    }
}
