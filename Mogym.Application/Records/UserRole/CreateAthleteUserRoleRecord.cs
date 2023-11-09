using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.UserRole
{
    public record CreateAthleteUserRoleRecord
    {
        public int UserId { get; init; }
        public int RoleId { get; init; } = 4;//role id athlete
    }
}
