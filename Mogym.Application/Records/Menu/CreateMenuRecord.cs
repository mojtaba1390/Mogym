using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Menu
{
    public record CreateMenuRecord
    {
        public string? EnglishName { get; init; }
        public string? PersianName { get; init; }
        public EnumYesNo? IsActive { get; init; }
        public int? ParentId { get; init; }
        public string Link { get; init; }
        public EnumYesNo? HasParentInPermission { get; set; }
        public int? PermissionParentId { get; set; }

    }
}
