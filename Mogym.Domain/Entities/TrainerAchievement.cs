using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class TrainerAchievement:BaseEntity
    {
        public string Title { get; set; }
        public int? Date { get; set; }

        public int TrainerProfileId { get; set; }

        public TrainerProfile TrainerAchievement_TrainerProfile { get; set; }
    }
}
