using Mogym.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Role
{
    public class RoleRecord
    {
        public int? Id { get; init; }
        public string EnglishName { get; init; }
        public string PersianName { get; init; }
        public EnumYesNo IsActive { get; init; }
    }
}
