using Mogym.Application.Records.TrainerAchievement;
using Mogym.Application.Records.TrainerPlanCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Profile
{
    public record TrainerProfileDetailRecord
    {
        public int? TrainerId { get; set; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Biography { get; init; }
        public string? UserName { get; init; }
        public string? ProfilePic { get; set; }
        public List<TrainerAchievementRecord> TrainerAchievementRecords { get; init; }
        public List<TrainerPlanCostRecord> TrainerPlanCostRecords { get; init; }
    }
}
