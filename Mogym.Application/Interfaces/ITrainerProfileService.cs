﻿using System;
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
        Task<TrainerProfileDetailRecord?> GetByUserName(string username);
        Task<CreateTrainerProfileRecord> GetByUserId(int userId);

		void Update(CreateTrainerProfileRecord trainerInfo);
        Task<CreateTrainerProfileRecord> GetEntityByUserId(int userId);
        bool IsAnyUserNameExist(string? username);
        Task<TrainerProfileRecord?> GetById(int trainerId);
        Task<CreateQuestionRecord?> GetTrainerForCreateQuestion(int trainerId);
        Task<TrainerProfile?> GetCurrentUserTrainer();
        Task<Tuple<int,int,List<TrainersRecord>>> GetAllTrainers(int? page, string search, string sort);
        Task<ConfirmAnswerQuestionRecord> GetConfirmAnswerQuestion(int trainerId,int trainerPlanId);
        Task<List<LastTrainersForHomePageRecord>> GetLastTrainersForHomepage();
        Task<int> GetAllTrainersCount();
        Task<Tuple<int, int, List<TrainersRecord>>> Search(string searchText);
        Task<Tuple<int, int, List<TrainersRecord>>> SortByNewest();
        Task<Tuple<int, int, List<TrainersRecord>>> SortBySentPlan();
    }
}
