using Mogym.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.TrainerPlanCost;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using Mogym.Application.Records.TrainerAchievement;
using Mogym.Common;
using Mogym.Domain.Entities;

namespace Mogym.Application.Services
{
    public class TrainerPlanCostService: ITrainerPlanCostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;

        public TrainerPlanCostService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<TrainerPlanCostRecord>> GetListByUserId(int userId)
        {
            try
            {
                var lst = _unitOfWork.TrainerProfileRepository.Find(x => x.UserId == userId)
                    .AsNoTracking()
                    .Include(x => x.TrainerPlanCosts);
                return _mapper.Map<List<TrainerPlanCostRecord>>(lst.SelectMany(x => x.TrainerPlanCosts));
            }
            catch (Exception ex)
            {
                var message = $"GetListByUserId in TrainerPlanCostService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task Create(int userId, CreateTrainerCostsRecord model)
        {
            try
            {
                var trainerCost = _mapper.Map<TrainerPlanCost>(model);
                trainerCost.TrainerProfileId = GetTrainerProfileIdByUserId(userId).Result.Id;
                await _unitOfWork.TrainerPlanCostRepository.AddAsync(trainerCost);
            }
            catch (Exception ex)
            {
                var message = $"Create in TrainerPlanCostService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public bool IsThereAnyEntityWithTrainerProfileIdAndPlanType(int trainerProfileId, int? trainerPlan)
        {
            return _unitOfWork.TrainerPlanCostRepository.Find(x =>
                x.TrainerProfileId == trainerProfileId && x.TrainerPlan == (EnumTrainerPlan)trainerPlan.Value).Any();
        }

        public void Delete(int id)
        {
            try
            {
                var entity = _unitOfWork.TrainerPlanCostRepository.GetById(id);
                _unitOfWork.TrainerPlanCostRepository.Delete(entity);
            }
            catch (Exception ex)
            {
                var message = $"Delete in TrainerPlanCostService";
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
