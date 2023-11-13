using Mogym.Application.Records.TrainerAchievement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TrainerPlanCost;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface ITrainerPlanCostService
    {
        Task<List<TrainerPlanCostRecord>> GetListByUserId(int userId);
        Task Create(int userId, CreateTrainerCostsRecord model);
        bool IsThereAnyEntityWithTrainerProfileIdAndPlanType(int trainerProfileId, int? trainerPlan);
    }
}
