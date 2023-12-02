using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Question;
using Mogym.Application.Records.Workout;
using Mogym.Common;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class PlanService:IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeriLogService _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly ITrainerProfileService _trainerProfileService;

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger,IHttpContextAccessor accessor,ITrainerProfileService trainerProfileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
            _trainerProfileService = trainerProfileService;
        }

        public async Task<List<PlanRecord>?> MyUnPaidPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId && x.PlanStatus==EnumPlanStatus.Registered)
                    .AsNoTracking()
                    .Include(x=>x.TrainerProfile_Plan)
                    .ThenInclude(x=>x.User)
                    .ToListAsync();
                return _mapper.Map<List<PlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetMyPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

        }

        public async Task UpdatePaidPicture(int planId,string paidPictureFileName)
        {
            try
            {
                var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
                plan.PaidPicture = paidPictureFileName;
                plan.PlanStatus = EnumPlanStatus.Paid;
                await _unitOfWork.PlanRepository.UpdateAsync(plan);
            }
            catch (Exception ex)
            {
                var message = $"UpdatePaidPicture in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<bool> IsThisPlanIdForThisCurrentUser(int planId)
        {
            var userId= _accessor.GetUser();
            return await _unitOfWork.PlanRepository.Find(x => x.UserId == userId && planId == planId).AnyAsync();
        }

        public async Task<AnswerQuestionRecord> GetAnswerQuestionWithPlanId(int planId)
        {
            try
            {
                var userId = _accessor.GetUser();
                var answerQuestion = await _unitOfWork.PlanRepository.Find(x => x.Id == planId)
                    .AsNoTracking()
                    .Include(x => x.AnsweQuestion_Plan)
                    .Select(x => x.AnsweQuestion_Plan)
                    .FirstOrDefaultAsync();
                return _mapper.Map<AnswerQuestionRecord>(answerQuestion);
            }
            catch (Exception ex)
            {
                var message = $"GetAnswerQuestionWithPlanId in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }

            return null;
        }

        public async Task<List<PaidPlanRecorrd>?> GetPaidPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var trainer = await _trainerProfileService.GetCurrentUserTrainer();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.TrainerId == trainer.Id && x.PlanStatus==EnumPlanStatus.Paid)
                    .AsNoTracking()
                    .Include(x => x.User_Plan)
                    .ToListAsync();
                return _mapper.Map<List<PaidPlanRecorrd>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetPaidPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        [HttpPost]
        public async Task ApprovePlan(int planId)
        {
            try
            {
                var plan = _unitOfWork.PlanRepository.GetById(planId);
                plan.PlanStatus = EnumPlanStatus.TrainerApprovment;
                await _unitOfWork.PlanRepository.UpdateAsync(plan);
            }
            catch (Exception ex)
            {
                var message = $"ApprovePlan in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<ApprovePlanRecord>?> GetApprovePlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var trainer = await _trainerProfileService.GetCurrentUserTrainer();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.TrainerId == trainer.Id && x.PlanStatus == EnumPlanStatus.TrainerApprovment)
                    .AsNoTracking()
                    .Include(x => x.User_Plan)
                    .ToListAsync();
                return _mapper.Map<List<ApprovePlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetApprovePlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<PlanDetailsRecord> GetPlanDetails(int planId)
        {
            try
            {
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.Id == planId)
                    .AsNoTracking()
                    .Include(x => x.Workouts)
                    .Include(x=>x.Meals)
                    .Include(x=>x.SupplimentPlans)
                    .FirstOrDefaultAsync();
                return _mapper.Map<PlanDetailsRecord>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetPlanDetails in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task AddDescription(int planId, string description)
        {
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
            plan.Description = description;
            await _unitOfWork.PlanRepository.UpdateAsync(plan);
        }

        public async Task SendPlan(int planId)
        {
            try
            {
                var plan = _unitOfWork.PlanRepository.GetById(planId);
                plan.PlanStatus = EnumPlanStatus.Sent;
                await _unitOfWork.PlanRepository.UpdateAsync(plan);
            }
            catch (Exception ex)
            {
                var message = $"SendPlan in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<SentPlanRecord>?> GetSentPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var trainer = await _trainerProfileService.GetCurrentUserTrainer();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.TrainerId == trainer.Id && x.PlanStatus == EnumPlanStatus.Sent)
                    .AsNoTracking()
                    .Include(x => x.User_Plan)
                    .ToListAsync();
                return _mapper.Map<List<SentPlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetSentPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<PlanRecord>> GetMyPaidPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId && x.PlanStatus == EnumPlanStatus.Paid)
                    .AsNoTracking()
                    .Include(x => x.TrainerProfile_Plan)
                    .ThenInclude(x => x.User)
                    .ToListAsync();
                return _mapper.Map<List<PlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"GetMyPaidPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<PlanRecord>> MyApprovedPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId && x.PlanStatus == EnumPlanStatus.TrainerApprovment)
                    .AsNoTracking()
                    .Include(x => x.TrainerProfile_Plan)
                    .ThenInclude(x => x.User)
                    .ToListAsync();
                return _mapper.Map<List<PlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"MyApprovedPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }

        public async Task<List<PlanRecord>> MyRecivedPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId && x.PlanStatus == EnumPlanStatus.Sent)
                    .AsNoTracking()
                    .Include(x => x.TrainerProfile_Plan)
                    .ThenInclude(x => x.User)
                    .ToListAsync();
                return _mapper.Map<List<PlanRecord>>(plans);
            }
            catch (Exception ex)
            {
                var message = $"MyRecivedPlans in PlanService";
                _logger.LogError(message, ex.InnerException);
                throw ex;
            }
        }
    }
}
