using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Role;

namespace Mogym.Application.AutoMapper.Role
{
    public class Role_RoleRecord_Reverse:Profile
    {
        public Role_RoleRecord_Reverse()
        {
            CreateMap<Domain.Entities.Role, RoleRecord>()
                .ForMember(x=>x.EnglishName,frm=>frm.MapFrom(z=>z.EnglishName))
                .ForMember(x=>x.PersianName,frm=>frm.MapFrom(z=>z.PersianName))
                .ReverseMap();

        }
    }
}
