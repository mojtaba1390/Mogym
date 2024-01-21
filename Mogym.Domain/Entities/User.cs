using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            Plans = new HashSet<Plan>();
            Creators = new HashSet<Ticket>();
            Assigns = new HashSet<Ticket>();
            TicketDetails = new HashSet<TicketDetail>();

            Comments = new HashSet<Comment>();
        }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? NationalCode { get; set; }
        public EnumGender? Gender { get; set; }
        public string Mobile { get; set; }
        public EnumStatus Status { get; set; }
        public Guid UniqeUserName { get; set; }
        public string? BirthDay { get; set; }
        public string? SmsConfirmCode { get; set; }

        public string? Email { get; set; }
        public string? ProfilePic { get; set; }


        public TrainerProfile TrainerProfile { get; set; }


        #region Collections
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Plan> Plans { get; set; }

        [InverseProperty("User_Creator")]
        public ICollection<Ticket> Creators { get; set; }
        [InverseProperty("User_Assign")]
        public ICollection<Ticket> Assigns { get; set; }

        public ICollection<TicketDetail> TicketDetails { get; set; }
        public ICollection<Comment> Comments { get; set; }

        #endregion
    }
}