using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mogym.Application.Interfaces;
using Mogym.Common;
using Mogym.Infrastructure;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Mogym.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public DiscountService(IUnitOfWork unitOfWork,
            IMapper mapper, IHttpContextAccessor accessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _accessor = accessor;
        }
        public async Task<Tuple<int, string, string>> GetDiscountPrice(string discountText, int trainingPlanId)
        {
            try
            {
                var currentUserId = _accessor.GetUser();



                //TODO: فعلا برای طرح تخفیف به اسم مربی ها متد زیر کامنت شد. بعدا برای اینکه برای هر یوزر کد تخفیف مخصوص تعریف بشه دوباره باید این قسمت رو از کامنت در بیاریم

                //var discount = await _unitOfWork.DiscountRepository
                //    .Find(x => x.DiscountText.Trim() == discountText && x.UserDiscounts.Any(z=>z.UserId==currentUserId))
                //    .Include(x=>x.UserDiscounts)
                //    .FirstOrDefaultAsync(); 


                var discount = await _unitOfWork.DiscountRepository
                    .Find(x => x.DiscountText.Trim() == discountText)
                    .FirstOrDefaultAsync();


                if (discount != null)
                {
                    //TODO:این متد هم به دلیل متد بالا کامنت شده

                    var useThisDiscountList = await _unitOfWork.DiscountUseRepository
                        .Find(x => x.DiscountId == discount.Id && x.UserId == currentUserId)
                        .ToListAsync();


                    var isThisUserUseDiscount = useThisDiscountList.Any(x => x.UserId == currentUserId);

                    if (!isThisUserUseDiscount)
                    {
                        if (discount.UseCount != null && useThisDiscountList.Count <= discount.UseCount)
                        {
                            if (discount.ActiveDate > DateTime.Now)
                            {
                                double lastPrice = 0;
                                var trainingPlanCost = await _unitOfWork.TrainerPlanCostRepository
                                    .Find(x => x.Id == trainingPlanId)
                                    .FirstAsync();

                                var trainerProfileId = trainingPlanCost.TrainerProfileId;

                                if (discount.TrainerId == trainerProfileId)
                                {
                                    if (discount.DiscountType == EnumDiscountType.Percent)
                                    {
                                        var discountPrice = (trainingPlanCost.OriginalCost * discount.Value) / 100;
                                        lastPrice = trainingPlanCost.OriginalCost.Value - discountPrice.Value;
                                    }
                                    else
                                    {
                                        lastPrice = Convert.ToInt32(trainingPlanCost.OriginalCost) - discount.Value;
                                    }

                                    return new Tuple<int, string, string>(200, discount.Id.ToString(), lastPrice.ToString("N0"));
                                }
                                else
                                    return new Tuple<int, string, string>(400, "کد استفاده شده مربوط به این مربی نمی باشد", String.Empty);

                            }
                            else
                                return new Tuple<int, string, string>(400, "بازه زمانی استفاده از کد به پایان رسیده است", String.Empty);
                        }
                        else
                            return new Tuple<int, string, string>(400, "تعداد استفاده از این کد تکمیل شده است", String.Empty);
                    }
                    return new Tuple<int, string, string>(400, "این کد قبلا استفاده شده است", String.Empty);



                }
                else
                    return new Tuple<int, string, string>(500, "کد وارد شده یافت نشد", String.Empty);
            }
            catch (Exception ex)
            {

            }
            return new Tuple<int, string, string>(500, "خطا", String.Empty);

        }
    }
}
