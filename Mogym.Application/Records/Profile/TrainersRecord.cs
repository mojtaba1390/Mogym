using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TrainerAchievement;

namespace Mogym.Application.Records.Profile
{
    public record TrainersRecord
    {
        public int Id { get; init; }
        public string  FullName { get; init; }
        public string  ProfilePic { get; init; }
        public string UserName { get; init; }
        public int? SentPlanCount { get; set; }
        public int? CommentCount { get; set; }
        public float? Score { get; set; }
        public List<TrainerAchievementRecord> TrainerAchievementRecords { get; set; }

    }
}
