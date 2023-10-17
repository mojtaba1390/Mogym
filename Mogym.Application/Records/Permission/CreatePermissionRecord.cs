using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.Records.Permission
{
    public record CreatePermissionRecord
    {
        public string? EnglishName { get; init; }
        public string? PersianName { get; init; }
        public EnumYesNo? IsActive { get; init; }
        public int? ParentId { get; init; }

        public List<PermissionRecord> Permissions { get; init; }
    }
}
