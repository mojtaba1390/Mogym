using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Permission;
using Mogym.Application.Records.Role;

namespace Mogym.Application.AutoMapper.Permission
{
    public class Permission_PermissionRecord_Reverse:Profile
    {
        public Permission_PermissionRecord_Reverse()
        {
            CreateMap<Domain.Entities.Permission, PermissionRecord>()
                .ForMember(x => x.EnglishName, frm => frm.MapFrom(z => z.EnglishName))
                .ForMember(x => x.PersianName, frm => frm.MapFrom(z => z.PersianName))
                .ReverseMap();

        }
    }
}
