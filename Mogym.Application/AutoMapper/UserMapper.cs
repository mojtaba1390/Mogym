using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.User;
using Mogym.Domain.Common;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper
{
    public class UserMapper:Profile
    {
        public UserMapper()
        {
            CreateMap<RegisterUserRecord, User>()
                .ForMember(x => x.Mobile, frm => frm.MapFrom(z => z.Mobile))
                .ForMember(x => x.Status, frm => frm.MapFrom(z => EnumStatus.WaitingForSmsConfirm))
                .ForMember(x => x.UniqeUserName, frm => frm.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.SmsConfirmCode, frm => frm.MapFrom(z => new Random().Next(10000, 99999)));

        }
    }
}
