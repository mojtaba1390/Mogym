using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TicketDetail;

namespace Mogym.Application.AutoMapper.TicketDetail
{
    public class TicketDetail_TicketDetailRecord:global::AutoMapper.Profile
    {
        public TicketDetail_TicketDetailRecord()
        {
            CreateMap<Domain.Entities.TicketDetail, TicketDetailRecord>()
                .ForMember(x=>x.UserId,
                    z=>z.MapFrom(a=>a.UserId))
                .ForMember(x => x.FullName,
                    z => z.MapFrom(a => a.User_TiketDetail.FirstName + " " + a.User_TiketDetail.LastName))
                .ForMember(x => x.Message,
                    z => z.MapFrom(a => a.Message))
                .ForMember(x => x.InsertDate,
                    z => z.MapFrom(a => Mogym.Common.Helper.GetPersianDateDetail(a.InsertDate)));
        }
    }
}
