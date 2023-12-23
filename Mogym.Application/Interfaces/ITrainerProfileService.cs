using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Profile;
using Mogym.Application.Records.Question;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface ITrainerProfileService
    {
        Task<TrainerProfileRecord?> GetByUserName(string username);
        Task<CreateTrainerProfileRecord> GetByUserId(int userId);

		void Update(CreateTrainerProfileRecord trainerInfo);
        Task<CreateTrainerProfileRecord> GetEntityByUserId(int userId);
        bool IsAnyUserNameExist(string? username);
        Task<TrainerProfileRecord?> GetById(int trainerId);
        Task<CreateQuestionRecord?> GetTrainerForCreateQuestion(int trainerId);
        Task<TrainerProfile?> GetCurrentUserTrainer();
        Task<List<TrainersRecord>> GetAllTrainers();
        Task<ConfirmAnswerQuestionRecord> GetConfirmAnswerQuestion(int trainerId,int trainerPlanId);
        Task<List<LastTrainersForHomePageRecord>> GetLastTrainersForHomepage();
    }
}
