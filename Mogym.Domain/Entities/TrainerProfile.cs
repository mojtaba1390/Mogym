using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class TrainerProfile:BaseEntity
    {
        public TrainerProfile()
        {
            TrainerPlanCosts = new HashSet<TrainerPlanCost>();
            TrainerAchievements = new HashSet<TrainerAchievement>();
            Plans = new HashSet<Plan>();
            Comments = new HashSet<Comment>();
        }

        public string? Biography { get; set; }

        public string? CartNumber { get; set; }
        public string? CartOwnerName { get; set; }

        public string? NationalCartPic { get; set; }
        public string? TrainingCertificatePic { get; set; }
        public string? WorkingCertificatePic { get; set; }
        public string? ChampCertificatePic1 { get; set; }
        public string? ChampCertificatePic2 { get; set; }
        public string? ChampCertificatePic3 { get; set; }
        public string? GraduationCertificatePic { get; set; }
        public string? StudingCertificatePic { get; set; }
        public int? SignupRate { get; set; }
        public float? AvgUserRate { get; set; }


        public int UserId { get; set; } 

        //relations
        public User User { get; set; }


        public ICollection<TrainerPlanCost> TrainerPlanCosts { get; set; }
        public ICollection<TrainerAchievement> TrainerAchievements { get; set; }
        public ICollection<Plan> Plans { get; set; }
        public ICollection<Comment> Comments { get; set; }



    }
}
