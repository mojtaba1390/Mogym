using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.RolePermission;

namespace Mogym.Application.AutoMapper.RolePermission
{
    public class RolePermission_RolePermissionRecord:global::AutoMapper.Profile
    {
        public RolePermission_RolePermissionRecord()
        {
            CreateMap<Domain.Entities.RolePermission, RolePermissionRecord>()
                .ForMember(x => x.RoleId,
                    z => z.MapFrom(a => a.RoleId))
                .ForMember(x => x.PermissionId,
                    z => z.MapFrom(a => a.PermissionId));
        }
    }
}
