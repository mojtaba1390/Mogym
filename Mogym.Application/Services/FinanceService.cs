using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Finance;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Profile;
using Mogym.Common;
using Mogym.Domain.Entities;
using Mogym.Infrastructure;

namespace Mogym.Application.Services
{
    public class FinanceService:IFinanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public FinanceService(IUnitOfWork unitOfWork,
            IMapper mapper, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<TrainerPaymentRecord> ApproveForPay(WaitForPayRecord model)
        {
            var currentUser = _accessor.GetUser();

            var plan = await _unitOfWork.PlanRepository.Where(x => x.Id == model.PlanId).FirstOrDefaultAsync();
            plan.PlanStatus = EnumPlanStatus.Registered;
            await _unitOfWork.PlanRepository.UpdateAsync(plan, false);

            if (model.DiscountId is not null)
            {
                var discountUse = new DiscountUse()
                {
                    UserId = currentUser,
                    DiscountId = model.DiscountId.Value,
                    UseDate = DateTime.Now
                };
                await _unitOfWork.DiscountUseRepository.AddAsync(discountUse, false);
            }

            var finance = new Finance()
            {
                PlanId = model.PlanId,
                TrainingPlanId = model.TrainingPlanId,
                DiscountId = model.DiscountId ?? 0,
                FinalPrice = model.LastPrice,
                FinanceStatus = EnumFinanceStatus.WaitForPay
            };
            await _unitOfWork.FinanceRepository.AddAsync(finance);

            var trainer = await _unitOfWork.TrainerProfileRepository.Find(x => x.Id == plan.TrainerId)
                .FirstOrDefaultAsync();

            //TODO:این قسمت از اوتو مپر اشتباه مپ شده ولی چون برای کارت به کارت هست و بعدا درگاه وصل میشه اوکی هست.
            var trainerPaymentDetail = _mapper.Map<TrainerPaymentRecord>(trainer);
            trainerPaymentDetail.Cost = model.LastPrice.ToString("N0");

            return trainerPaymentDetail;

        }

        public async Task<List<FinanceHistoryRecord>?> GetFinanceHistory()
        {
            try
            {
                var currentUserId = _accessor.GetUser();
                var finances =await _unitOfWork.FinanceRepository
                     .Find(x=>x.Plan_Finance.TrainerProfile_Plan.UserId==currentUserId)
                    .Include(x => x.Plan_Finance)
                    .ThenInclude(x => x.User_Plan)
                    .Include(x => x.Discount_Finance)
                    .Include(x=>x.Plan_Finance)
                    .ThenInclude(x=>x.TrainerProfile_Plan)
                    .ThenInclude(x=>x.TrainerPlanCosts)
                    .ToListAsync();

                return _mapper.Map<List<FinanceHistoryRecord>>(finances);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
