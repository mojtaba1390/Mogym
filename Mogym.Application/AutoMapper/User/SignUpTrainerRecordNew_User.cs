using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.User;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.User
{
    public class SignUpTrainerRecordNew_User:global::AutoMapper.Profile
    {
        public SignUpTrainerRecordNew_User()
        {
            CreateMap<SignUpTrainerRecordNew, Domain.Entities.User>()
                .ForMember(x => x.UniqeUserName, frm => frm.MapFrom(z => Guid.NewGuid()))
                .ForMember(x=>x.Password,
                    z=>z.MapFrom(a=> Common.Helper.HashString(a.Password)))
                .ForMember(x => x.Status,
                    z => z.MapFrom(a => EnumStatus.Active));
        }
    }
}
