using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Achievement:BaseEntity
    {
        public string Title { get; set; }
        public int? Date { get; set; }

        public int UserProfileId { get; set; }

        public UserProfile Achievement_UserProfile { get; set; }
    }
}
