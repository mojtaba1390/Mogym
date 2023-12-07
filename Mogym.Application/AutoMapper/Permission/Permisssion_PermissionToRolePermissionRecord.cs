using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Permission;

namespace Mogym.Application.AutoMapper.Permission
{
    public class Permisssion_PermissionToRolePermissionRecord:global::AutoMapper.Profile
    {
        public Permisssion_PermissionToRolePermissionRecord()
        {
            CreateMap<Domain.Entities.Permission, PermissionToRolePermissionRecord>()
                .ForMember(x => x.Id,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.Title,
                    z => z.MapFrom(a => a.PersianName))
                .ForMember(x => x.RolePermissionRecords,
                    z => z.MapFrom(a => a.RolePermissions));
        }
    }
}
