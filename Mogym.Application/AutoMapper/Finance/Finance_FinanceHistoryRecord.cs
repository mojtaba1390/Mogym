using Mogym.Application.Records.Finance;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.Finance
{
    public class Finance_FinanceHistoryRecord:global::AutoMapper.Profile
    {
        public Finance_FinanceHistoryRecord()
        {
            CreateMap<Domain.Entities.Finance, FinanceHistoryRecord>()
                .ForMember(x => x.AthleteFullName,
                    z => z.MapFrom(a => a.Plan_Finance.User_Plan.FirstName + " " + a.Plan_Finance.User_Plan.LastName))
                .ForMember(x => x.TrainingPlanName,
                    z => z.MapFrom(a =>
                        a.Plan_Finance.TrainerProfile_Plan.TrainerPlanCosts
                            .First(q => q.Id == a.Plan_Finance.AnsweQuestion_Plan.TrainerPlan).TrainerPlan
                            .GetEnumDescription()))
                .ForMember(x => x.InsertDate,
                    z => z.MapFrom(a =>a.InsertDate.GetShamsiDate()))
                .ForMember(x => x.TrainingPlanActualPrice,
                    z => z.MapFrom(a =>
                        a.Plan_Finance.TrainerProfile_Plan.TrainerPlanCosts
                            .First(q => q.Id == a.Plan_Finance.AnsweQuestion_Plan.TrainerPlan).OriginalCost.Value
                            .ToString("N0") + "ریال "))
                .ForMember(x => x.TrainingPlanPayPrice,
                    z => z.MapFrom(a => a.FinalPrice.ToString("N0") + " ریال"))
                .ForMember(x => x.DiscountCode,
                    z => z.MapFrom(a => a.DiscountId != null ? a.Discount_Finance.DiscountText : string.Empty))
                .ForMember(x => x.DiscountValue,
                    z => z.MapFrom(a =>
                        a.DiscountId != null
                            ? a.Discount_Finance.Value + " " + a.Discount_Finance.DiscountType.GetEnumDescription()
                            : string.Empty))
                .ForMember(x => x.PayStatus,
                    z => z.MapFrom(a => a.FinanceStatus.GetEnumDescription()));
        }
    }
}
