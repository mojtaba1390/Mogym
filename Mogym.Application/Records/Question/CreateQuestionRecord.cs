using Microsoft.AspNetCore.Http;
using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TrainerPlanCost;

namespace Mogym.Application.Records.Question
{
    public record CreateAttendanceClientQuestionRecord
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Mobile { get; init; }
        public int Gender { get; init; }
        public int Age { get; init; }
        public int Height { get; init; }
        public double Weight { get; init; }
        public double? Waist { get; init; }
        public double? Biceps { get; init; }
        public double? Chest { get; init; }
        public double? Thigh { get; init; }
        public double? Fist { get; init; }
        public int? DailyAvtivity { get; init; }
        public int? NightWork { get; init; }
        public string? OutOfGymActivity { get; init; }
        public string? Disease { get; init; }
        public string? Medicine { get; init; }
        public string? Treated { get; init; }
        public string? Injury { get; init; }
        public int? HeartDisease { get; init; }
        public int? DiabetesAsthmaHypoglycemia { get; init; }
        public int? Smoke { get; init; }
        public int? SessionsInWeek { get; init; }
        public string? Expection { get; init; }
        public IFormFile? FrontPic { get; init; }
        public IFormFile? BackPic { get; init; }
        public IFormFile? LeftPic { get; init; }
        public IFormFile? RightPic { get; init; }

        public List<TrainerPlanCostRecord>? TrainerPlanCosts { get; init; }


        public int TrainerPlanId { get; init; }
        public int TrainerId { get; init; }

        public string? TrainerFullName { get; init; }
        public int QuestionId { get; init; }
        public int PlanId { get; init; }
        public string Code { get; init; }

        public int Target { get; set; }
    }
}
