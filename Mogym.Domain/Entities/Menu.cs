using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Domain.Common;

namespace Mogym.Domain.Entities
{
    public class Menu:BaseEntity
    {
        public Menu()
        {
            Menus = new HashSet<Menu>();
        }
        public string EnglishName { get; set; }
        public string PersianName { get; set; }
        public EnumYesNo IsActive { get; set; }

        public int? ParentId { get; set; }

        public Menu Menu_Menu { get; set; }

        public ICollection<Menu> Menus { get; set; }
    }
}
