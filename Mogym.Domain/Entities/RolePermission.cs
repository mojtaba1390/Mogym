using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class RolePermission:BaseEntity
    {

        public int RoleId { get; set; }

        public int PermissionId { get; set; }



        public Permission RolePermission_Permission { get; set; }

        public Role RolePermission_Role { get; set; }
    }
}
