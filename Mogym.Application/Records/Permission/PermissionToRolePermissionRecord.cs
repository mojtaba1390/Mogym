using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.RolePermission;

namespace Mogym.Application.Records.Permission
{
    public record PermissionToRolePermissionRecord
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public List<RolePermissionRecord> RolePermissionRecords { get; init; }
    }
}
