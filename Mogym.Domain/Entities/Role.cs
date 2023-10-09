using Mogym.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Mogym.Domain.Entities
{
    public class Role:BaseEntity
    {
        public Role()
        {
            Accessibilities = new HashSet<Accessibility>();
        }
        public string EnglishName { get; set; }
        public string PersianName { get; set; }
        public EnumYesNo IsActive { get; set; }

        public int UserId { get; set; }


        public User User { get; set; }

        public ICollection<Accessibility> Accessibilities { get; set; }
    }
}
