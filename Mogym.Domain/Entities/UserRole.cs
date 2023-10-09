using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class UserRole:BaseEntity
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }


        public User User { get; set; }

        public Role Role { get; set; }
    }
}
