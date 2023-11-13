using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TrainerAchievement;

namespace Mogym.Application.Interfaces
{
    public interface ITrainerAchievementService
    {
        Task<List<TrainerAchievementRecord>> GetListByTrainerProfileId(int trainerProfileId);
        Task<List<TrainerAchievementRecord>> GetListByUserId(int userId);
        Task Create(int userId, CreateTrainerAchievementRecord model);
        void Delete(int id);
    }
}
