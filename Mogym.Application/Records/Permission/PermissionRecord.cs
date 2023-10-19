using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.Records.Permission
{
    public class PermissionRecord
    {
        public int Id { get; init; }
        public string? EnglishName { get; init; }
        public string? PersianName { get; init; }
        public EnumYesNo? IsActive { get; init; }
        public int? ParentId { get; set; }
        public string? ParentName { get; set;}

    }
}
