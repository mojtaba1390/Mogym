using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class Plan:BaseEntity
    {
        public int TrainerId { get; set; }
        public int UserId { get; set; }
        public int AnserQuestionId { get; set; }
        public EnumPlanStatus PlanStatus { get; set; }


        public TrainerProfile TrainerProfile_Plan { get; set; }
        public User User_Plan { get; set; }
        public Question AnsweQuestion_Plan { get; set; }
    }
}
