using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.TrainerAchievement
{
    public record TrainerAchievementRecord
    {
        public int Id { get; init; }
        public string? Title { get; init; }
        public int? Date { get; init; }
    }

    public record CreateTrainerAchievementRecord:TrainerAchievementRecord
    {

    }
}
