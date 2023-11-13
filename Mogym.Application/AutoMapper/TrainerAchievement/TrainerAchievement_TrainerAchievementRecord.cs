using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TrainerAchievement;

namespace Mogym.Application.AutoMapper.TrainerAchievement
{
    public class TrainerAchievement_TrainerAchievementRecord:global::AutoMapper.Profile
    {
        public TrainerAchievement_TrainerAchievementRecord()
        {
            CreateMap<Domain.Entities.TrainerAchievement, TrainerAchievementRecord>().ReverseMap();
        }
    }
}
