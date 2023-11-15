using Mogym.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Question
{
    public record QuestionRecord
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public EnumGender Gender { get; init; }
        public int Age { get; init; }
        public int Height { get; init; }
        public double Weight { get; init; }
        public double? Waist { get; init; }
        public double? Biceps { get; init; }
        public double? Chest { get; init; }
        public double? Thigh { get; init; }
        public double? Fist { get; init; }
        public EnumDailyAvtivity? DailyAvtivity { get; init; }
        public EnumYesNo? NightWork { get; init; }
        public string? OutOfGymActivity { get; init; }
        public string? Disease { get; init; }
        public string? Medicine { get; init; }
        public string? Treated { get; init; }
        public string? Injury { get; init; }
        public EnumYesNo? HeartDisease { get; init; }
        public EnumYesNo? DiabetesAsthmaHypoglycemia { get; init; }
        public EnumYesNo? Smoke { get; init; }
        public EnumSessionsInWeek? SessionsInWeek { get; init; }
        public string? Expection { get; init; }
        public string? FrontPic { get; init; }
        public string? BackPic { get; init; }
        public string? LeftPic { get; init; }
        public string? RightPic { get; init; }

        public EnumTrainerPlan TrainerPlan { get; init; }
    }
}
