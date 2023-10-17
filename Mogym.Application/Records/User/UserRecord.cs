using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Permission;
using Mogym.Application.Records.Role;
using Mogym.Common;

namespace Mogym.Application.Records.User
{
    public class UserRecord
    {
        public int? Id { get; init; }
        public string? UserName { get; init; }
        public string? Password { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string NationalCode { get; init; }
        public EnumGender? Gender { get; init; }
        public string Mobile { get; init; }
        public EnumStatus Status { get; init; }
        public Guid UniqeUserName { get; init; }
        public string? BirthDay { get; init; }
        public string SmsConfirmCode { get; init; }

        public string? Email { get; init; }

        public List<RoleRecord> Roles { get; init; }
        public List<PermissionRecord> Permissions { get; init; }

    }

}
