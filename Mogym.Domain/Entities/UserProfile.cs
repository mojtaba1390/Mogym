using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class UserProfile:BaseEntity
    {
        public string? Description { get; set; }
        public User User { get; set; }
    }
}
