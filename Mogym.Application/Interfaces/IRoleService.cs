using Mogym.Application.Records.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Interfaces
{
    public interface IRoleService
    {
        RoleRecord GetRoleByName(string roleName);
    }
}
