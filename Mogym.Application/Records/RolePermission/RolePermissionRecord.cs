using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.RolePermission
{
    public record RolePermissionRecord
    {
        public int PermissionId { get; init; }
        public int RoleId { get; init; }
    }
}
