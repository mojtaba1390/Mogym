using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.User;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.User
{
    public class OtpLoginRecord_User:global::AutoMapper.Profile
    {
        public OtpLoginRecord_User()
        {
            CreateMap<OTPLoginRecord,Domain.Entities.User>()
                .ForMember(x => x.Status, frm => frm.MapFrom(z => EnumStatus.WaitingForSmsConfirm))
                .ForMember(x => x.UniqeUserName, frm => frm.MapFrom(z => Guid.NewGuid()))
                .ForMember(x=>x.SmsConfirmCode,
                    z=>z.MapFrom(a=> new Random().Next(10000, 99999).ToString()))
                .ForMember(x => x.Password,
                    z => z.MapFrom(a => Common.Helper.HashString("12345")));
        }
    }
}
