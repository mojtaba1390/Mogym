using AutoMapper;
using Mogym.Application.Records.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mogym.Application.AutoMapper.User
{
    public class User_ConfirmSmsCode : global::AutoMapper.Profile
    {
        public User_ConfirmSmsCode()
        {
            CreateMap<Domain.Entities.User, ConfirmSmsRecord>()
                .ForMember(x => x.ConfirmCode, frm => frm.MapFrom(z => z.SmsConfirmCode))
                .ForMember(x => x.Mobile, frm => frm.MapFrom(z => z.Mobile));
        }

    }
}
