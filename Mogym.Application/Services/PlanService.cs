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
using Mogym.Application.Records.Plan;
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

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper, ISeriLogService logger,IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task<List<PlanRecord>?> GetMyPlans()
        {
            try
            {
                var userId = _accessor.GetUser();
                var plans = await _unitOfWork.PlanRepository
                    .Find(x => x.UserId == userId)
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
