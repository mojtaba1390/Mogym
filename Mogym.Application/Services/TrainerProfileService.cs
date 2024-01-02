using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Profile;
using Mogym.Application.Records.Question;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class TrainerProfileService:ITrainerProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;

        public TrainerProfileService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger,IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task<TrainerProfileRecord?> GetByUserName(string username)
        {
            try
            {
                var trainer= await _unitOfWork.TrainerProfileRepository
                    .Find(x => x.User.UserName == username.Trim() && x.User.UserRoles.Any(z => z.RoleId == 3))
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x=>x.TrainerAchievements)
                    .Include(x=>x.TrainerPlanCosts)
                    .FirstOrDefaultAsync();
                if(trainer is not null)
                    return _mapper.Map<TrainerProfileRecord?>(trainer);

            }
            catch (Exception ex)
            {
                var message = $"GetByUserName in TrainerProfile Service";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }



        public async Task<CreateTrainerProfileRecord> GetByUserId(int userId)
        {
	        try
	        {
		        var trainer= await _unitOfWork.TrainerProfileRepository
			        .Find(x => x.User.Id == userId && x.User.UserRoles.Any(z => z.RoleId == 3))
			        .AsNoTracking()
			        .Include(x => x.User)
			        .ThenInclude(x => x.UserRoles)
			        .FirstOrDefaultAsync();

		        return _mapper.Map<CreateTrainerProfileRecord>(trainer);
	        }
	        catch (Exception ex)
	        {
		        var message = $"GetByUserId in TrainerProfile Service";
		        _logger.LogError(message, ex.InnerException);
		        throw ex;
	        }

	        return null;
        }



		public void Update(CreateTrainerProfileRecord trainerInfoRecord)
        {
            try
            {
                var trainerProfileOld = _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerInfoRecord.Id)
                    .Include(x => x.User)
                    .FirstOrDefault();

                var trainerInfo = _mapper.Map(trainerInfoRecord, trainerProfileOld);
                //var user = _mapper.Map(trainerInfoRecord, trainerProfileOld.User);

                  _unitOfWork.TrainerProfileRepository.Update(trainerInfo);
                  //_unitOfWork.UserRepository.Update(user);
            }
            catch (Exception ex)
            {
                var message = $"Update in TrainerProfile Service";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<CreateTrainerProfileRecord> GetEntityByUserId(int userId)
        {
            var trainerProfile = await _unitOfWork.TrainerProfileRepository.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            return  _mapper.Map<CreateTrainerProfileRecord>(trainerProfile);
        }

        public bool IsAnyUserNameExist(string? username)
        {
            return _unitOfWork.TrainerProfileRepository.Find(x => x.User.UserName == username.Trim())
                .AsNoTracking()
                .Include(x => x.User)
                .Any();
        }

        public async Task<TrainerProfileRecord?> GetById(int trainerId)
        {
            var trainer= await _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerId)
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync();
            if (trainer is not null)
                return _mapper.Map<TrainerProfileRecord>(trainer);

            return null;


        }

        public async Task<CreateQuestionRecord?> GetTrainerForCreateQuestion(int trainerId)
        {
            var trainer = await _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerId)
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x=>x.TrainerPlanCosts)
                .FirstOrDefaultAsync();
            if (trainer is not null)
                return _mapper.Map<CreateQuestionRecord>(trainer);

            return null;
        }

        public async Task<TrainerProfile?> GetCurrentUserTrainer()
        {
            var userId = _accessor.GetUser();
            return await _unitOfWork.TrainerProfileRepository.Find(x => x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<List<TrainersRecord>> GetAllTrainers()
        {
            var trainers = await  _unitOfWork.TrainerProfileRepository
                .GetAll()
                .Include(x=>x.User)
                .OrderByDescending(x => x.Id)
                .ToListAsync() ;

            return _mapper.Map<List<TrainersRecord>>(trainers);
        }

        public async Task<ConfirmAnswerQuestionRecord> GetConfirmAnswerQuestion(int trainerId,int trainerPlanId)
        {
            var trainer = await _unitOfWork.TrainerProfileRepository.Find(x => x.Id == trainerId)
                .Include(x => x.User)
                .Include(x => x.TrainerPlanCosts.Where(z => z.Id == trainerPlanId))
                .Include(x => x.User).FirstOrDefaultAsync();
            return _mapper.Map<ConfirmAnswerQuestionRecord>(trainer);
        }

        public async Task<List<LastTrainersForHomePageRecord>> GetLastTrainersForHomepage()
        {
            var lastTrainers = await _unitOfWork.TrainerProfileRepository
                .GetAll()
                .Include(x=>x.User)
                .OrderByDescending(x=>x.Id)
                .Take(4)
                .ToListAsync();

            return   _mapper.Map<List<LastTrainersForHomePageRecord>>(lastTrainers);
        }
    }
}
