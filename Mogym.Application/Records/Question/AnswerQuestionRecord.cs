using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Question
{
    public record AnswerQuestionRecord
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string? Gender { get; init; }
        public int Age { get; init; }
        public int Height { get; init; }
        public double Weight { get; init; }
        public double? Waist { get; init; }
        public double? Biceps { get; init; }
        public double? Chest { get; init; }
        public double? Thigh { get; init; }
        public double? Fist { get; init; }
        public string? DailyAvtivity { get; init; }
        public string? NightWork { get; init; }
        public string? OutOfGymActivity { get; init; }
        public string? Disease { get; init; }
        public string? Medicine { get; init; }
        public string? Treated { get; init; }
        public string? Injury { get; init; }
        public string? HeartDisease { get; init; }
        public string? DiabetesAsthmaHypoglycemia { get; init; }
        public string? Smoke { get; init; }
        public string? SessionsInWeek { get; init; }
        public string? Expection { get; init; }
        public string? FrontPic { get; init; }
        public string? BackPic { get; init; }
        public string? LeftPic { get; init; }
        public string? RightPic { get; init; }

        public string TrainerPlan { get; set; }
        public string Mobile { get; set; }

        public string? Target { get; init; }

    }
}
