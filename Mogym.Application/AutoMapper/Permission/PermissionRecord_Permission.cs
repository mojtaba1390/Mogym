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
    public class PermissionRecord_Permission : global::AutoMapper.Profile
    {
        public PermissionRecord_Permission()
        {
            CreateMap<Domain.Entities.Permission, PermissionRecord>()
                .ForMember(x => x.EnglishName, frm => frm.MapFrom(z => z.EnglishName))
                .ForMember(x => x.PersianName, frm => frm.MapFrom(z => z.PersianName))
                .ForMember(x => x.IsActive, frm => frm.MapFrom(z => z.IsActive))
                .ForMember(x => x.Id, frm => frm.MapFrom(z => z.Id));


        }
    }
}
