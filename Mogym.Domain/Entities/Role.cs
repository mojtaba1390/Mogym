using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class Role:BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RolePermissions = new HashSet<RolePermission>();

        }
        public string EnglishName { get; set; }
        public string PersianName { get; set; }
        public EnumYesNo IsActive { get; set; }


        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

    }
}
