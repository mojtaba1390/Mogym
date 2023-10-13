using Mogym.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.User
{
    public class UserWithRoleAndPermissionForAfterAuhenticationRecord
    {
        public string? UserName { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public EnumGender? Gender { get; init; }
        public Guid UniqeUserName { get; init; }

        public List<RoleForAuthenticationRecord> Roles { get; init; }
    }


    public class RoleForAuthenticationRecord
    {
        public int Id { get; init; }
        public string EnglishName { get; init; }
        public string PersianName { get; init; }

        public List<PermissionForAuthenticationRecord> Permissions { get; init; }

    }

    public class PermissionForAuthenticationRecord
    {
        public int Id { get; init; }
        public string? EnglishName { get; init; }
        public string? PersianName { get; init; }
    }
}
