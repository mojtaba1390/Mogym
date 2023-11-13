using FluentValidation;
using Mogym.Application.Records.Menu;
using Mogym.Application.Records.TrainerPlanCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Interfaces;
using System.Security.Claims;

namespace Mogym.Application.Validation.TrainerCosts
{
    public class CreateTrainerCostsRecordValidate : AbstractValidator<CreateTrainerCostsRecord>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ITrainerPlanCostService _planCostService;
        private readonly ITrainerProfileService _trainerProfileService;

        public CreateTrainerCostsRecordValidate(IHttpContextAccessor accessor, ITrainerPlanCostService planCostService, ITrainerProfileService trainerProfileService)
        {
            #region Dependency

            _accessor = accessor;
            _planCostService = planCostService;
            _trainerProfileService = trainerProfileService;


            #endregion

            RuleFor(x => x.TrainerPlan).Must(x => !IsExistTrainerPlanForThisTrainer(x))
                .WithMessage("قبلا این نوع هزینه در لیست شما تعریف شده است");
        }

        private bool IsExistTrainerPlanForThisTrainer(int? trainerPlan)
        {
            var userIdString = _accessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int userId = Int32.Parse(userIdString);
            var trainerProfile =  _trainerProfileService.GetEntityByUserId(userId).Result;
            return _planCostService.IsThereAnyEntityWithTrainerProfileIdAndPlanType(trainerProfile.Id, trainerPlan);
        }
    }
}
