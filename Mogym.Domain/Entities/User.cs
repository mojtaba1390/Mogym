using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();


        }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string NationalCode { get; set; }
        public EnumGender? Gender { get; set; }
        public string Mobile { get; set; }
        public EnumStatus Status { get; set; }
        public Guid UniqeUserName { get; set; }
        public string? BirthDay { get; set; }
        public string SmsConfirmCode { get; set; }

        public string? Email { get; set; }
        public string? ProfilePic { get; set; }


        public TrainerProfile TrainerProfile { get; set; }
        #region Collections
        public ICollection<UserRole> UserRoles { get; set; }


        #endregion
    }
}
