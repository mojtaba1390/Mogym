using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Mogym.Domain.Common;

namespace Mogym.Domain.Entities
{
    public class Permission:BaseEntity
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
        }
        public string EnglishName { get; set; }
        public string PersianName { get; set; }
        public EnumYesNo IsActive { get; set; }

        public EnumYesNo? IsCreateMenu { get; set; }


        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
