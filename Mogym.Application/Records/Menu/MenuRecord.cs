using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.Records.Menu
{
    public class MenuRecord
    {
        public int? Id { get; init; }
        public string EnglishName { get; init; }
        public string PersianName { get; init; }
        public string Link { get; init; }
        public EnumYesNo IsActive { get; init; }

        public int? ParentId { get; init; }
    }
}
