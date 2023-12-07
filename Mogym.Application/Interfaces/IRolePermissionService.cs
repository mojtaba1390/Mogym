using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.RolePermission;

namespace Mogym.Application.Interfaces
{
    public interface IRolePermissionService
    {
        Task<List<RolePermissionRecord>> GetAll();
        Task DeleteByPermissionIdAndRoleId(int permissionId, int roleId);
        Task AddByPermissionIdAndRoleId(int permissionId, int roleId);
    }
}
