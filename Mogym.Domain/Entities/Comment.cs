using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public Comment()
        {
            Comments = new HashSet<Comment>();
        }
        public int PlanId { get; set; }
        public int? TrainerId { get; set; }
        public int? UserId { get; set; }
        public int? ParentId { get; set; }
        public string? Review { get; set; }
        public int? Rate { get; set; }



        public Plan Plan_Comment { get; set; }
        public TrainerProfile TrainerProfile_Comment { get; set; }
        public User User_Comment { get; set; }

        public Comment Comment_Parent { get; set; }


        public ICollection<Comment> Comments { get; set; }

    }
}