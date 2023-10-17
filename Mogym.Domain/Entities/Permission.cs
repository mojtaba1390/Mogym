using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class Permission:BaseEntity
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
            Permissions = new HashSet<Permission>();
        }
        public string EnglishName { get; set; }
        public string PersianName { get; set; }
        public EnumYesNo IsActive { get; set; }

        public int? ParentId { get; set; }


        public Permission Permission_Permission { get; set; }

        public ICollection<Permission> Permissions { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
