using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Profile;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface ITrainerProfileService
    {
        Task<TrainerProfile?> GetByUserName(string username);
        Task Update(TrainerProfileRecord trainerInfo);
        Task<TrainerProfileRecord> GetEntityByUserId(int userId);
        bool IsAnyUserNameExist(string? username);
    }
}
