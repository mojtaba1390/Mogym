using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.User;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.User
{
    public class LoginRecord_User:Profile
    {
        public LoginRecord_User()
        {
            CreateMap<LoginRecord, Domain.Entities.User>()
                .ForMember(x => x.Mobile, frm => frm.MapFrom(z => z.Mobile))
                .ForMember(x => x.Status, frm => frm.MapFrom(z => EnumStatus.WaitingForSmsConfirm))
                .ForMember(x => x.UniqeUserName, frm => frm.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.SmsConfirmCode, frm => frm.MapFrom(z => new Random().Next(10000, 99999)));
        }
    }
}
