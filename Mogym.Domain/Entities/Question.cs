using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class Question:BaseEntity
    {

        public Question()
        {
            Plans = new HashSet<Plan>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EnumGender Gender { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public double? Waist { get; set; }
        public double? Biceps { get; set; }
        public double? Chest { get; set; }
        public double? Thigh { get; set; }
        public double? Fist { get; set; }
        public EnumDailyAvtivity? DailyAvtivity { get; set; }
        public EnumYesNo? NightWork { get; set; }
        public string? OutOfGymActivity { get; set; }
        public string? Disease { get; set; }
        public string? Medicine { get; set; }
        public string? Treated { get; set; }
        public string? Injury { get; set; }
        public EnumYesNo? HeartDisease { get; set; }
        public EnumYesNo? DiabetesAsthmaHypoglycemia { get; set; }
        public EnumYesNo? Smoke { get; set; }
        public EnumSessionsInWeek? SessionsInWeek { get; set; }
        public string? Expection { get; set; }
        public string? FrontPic { get; set; }
        public string? BackPic { get; set; }
        public string? LeftPic { get; set; }
        public string? RightPic { get; set; }

        public EnumTrainerPlan TrainerPlan { get; set; }



        public ICollection<Plan> Plans { get; set; }
    }
}
