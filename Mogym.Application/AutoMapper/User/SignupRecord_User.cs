using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.User;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.User
{
    public class SignupRecord_User:global::AutoMapper.Profile
    {
        public SignupRecord_User()
        {
            CreateMap<SignupRecord, Domain.Entities.User>()
                .ForMember(x => x.Status, frm => frm.MapFrom(z => EnumStatus.Active))
                .ForMember(x => x.UniqeUserName, frm => frm.MapFrom(z => Guid.NewGuid()))
                .ForMember(x => x.FirstName,
                    z => z.MapFrom(a => a.FirstName))
                .ForMember(x => x.LastName,
                    z => z.MapFrom(a => a.LastName))
                .ForMember(x => x.Email,
                    z => z.MapFrom(a => a.Email))
                .ForMember(x => x.Password,
                    z => z.MapFrom(a => Common.Helper.HashString(a.Password)));
        }
    }
}
