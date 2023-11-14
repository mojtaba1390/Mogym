using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Profile;

namespace Mogym.Application.Validation.TrainerProfile
{
    public class TrainerProfileRecordValidate:AbstractValidator<TrainerProfileRecord>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ITrainerProfileService _trainerProfileService;


        public TrainerProfileRecordValidate(IHttpContextAccessor accessor,ITrainerProfileService trainerProfileService)
        {

            #region Dependency

            _accessor = accessor;
            _trainerProfileService = trainerProfileService;

            #endregion


            RuleFor(x => x.UserName)
                .Must(z => !IsAnyUserNameExist(z)).WithMessage("نام کاربری انتخابی قبلا در سیستم ثبت شده است")
                .Must(z => !IsDigitOrLetter(z)).WithMessage("نام کاربری فقط شامل حروف یا عدد باشد");


        }

        private bool IsDigitOrLetter(string? username)
        {
            return username.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private bool IsAnyUserNameExist(string? username)
        {
            return _trainerProfileService.IsAnyUserNameExist(username);
        }
    }
}
