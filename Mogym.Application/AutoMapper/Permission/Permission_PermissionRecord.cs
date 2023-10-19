using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Permission;
using Mogym.Application.Records.Role;

namespace Mogym.Application.AutoMapper.Permission
{
    public class Permission_PermissionRecord:Profile
    {
        public Permission_PermissionRecord()
        {
            CreateMap<Domain.Entities.Permission, PermissionRecord>()
                .ForMember(x => x.EnglishName, frm => frm.MapFrom(z => z.EnglishName))
                .ForMember(x => x.PersianName, frm => frm.MapFrom(z => z.PersianName))
                .ForMember(x => x.ParentId, frm => frm.MapFrom(z => z.ParentId))
                .ForMember(x => x.ParentName,
                    frm => frm.MapFrom(
                        z =>(z.Permission_Permission != null ? z.Permission_Permission.PersianName : "")));

        }
    }
}
