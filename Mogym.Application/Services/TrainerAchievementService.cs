using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.TrainerAchievement;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class TrainerAchievementService:ITrainerAchievementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;

        public TrainerAchievementService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<TrainerAchievementRecord>> GetListByTrainerProfileId(int trainerProfileId)
        {
            try
            {
                var lst = _unitOfWork.TrainerAchievementRepository.Find(x => x.TrainerProfileId == trainerProfileId)
                    .AsNoTracking();
                return  _mapper.Map<List<TrainerAchievementRecord>>(lst);
            }
            catch (Exception ex)
            {
                var message = $"GetListByTrainerProfileId in TrainerAchievementService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<TrainerAchievementRecord>> GetListByUserId(int userId)
        {
            try
            {
                var lst =  _unitOfWork.TrainerProfileRepository.Find(x=>x.UserId==userId)
                    .AsNoTracking()
                    .Include(x=>x.TrainerAchievements);
                return _mapper.Map<List<TrainerAchievementRecord>>(lst.SelectMany(x=>x.TrainerAchievements));
            }
            catch (Exception ex)
            {
                var message = $"GetListByUserId in TrainerAchievementService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task Create(int userId, CreateTrainerAchievementRecord model)
        {
            try
            {
                var trainerAchievement = _mapper.Map<TrainerAchievement>(model);
                trainerAchievement.TrainerProfileId = GetTrainerProfileIdByUserId(userId).Result.Id;
                await _unitOfWork.TrainerAchievementRepository.AddAsync(trainerAchievement);
            }
            catch (Exception ex)
            {
                var message = $"Create in TrainerAchievementService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var entity= _unitOfWork.TrainerAchievementRepository.GetById(id);
                _unitOfWork.TrainerAchievementRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                var message = $"Delete in TrainerAchievementService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        private async Task<TrainerProfile> GetTrainerProfileIdByUserId(int userId)
        {
            return await _unitOfWork.TrainerProfileRepository.Find(x => x.UserId == userId).FirstAsync();
        }
    }
}
