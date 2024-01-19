using FluentValidation;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Validation.User
{
    public class OTPLoginRecordValidate : AbstractValidator<OTPLoginRecord>
    {
        private readonly IUserService _userService;

        public OTPLoginRecordValidate(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("شماره موبایل نباید خالی باشد")
                .NotEmpty().WithMessage("شماره موبایل نباید خالی باشد")
                .Length(11).WithMessage("شماره موبایل باید ۱۱ رقم باشد")
                .Matches("^09\\d{9}$").WithMessage("فرمت شماره موبایل اشتباه می باشد");
                //.Must(x => !IsMobileExist(x)).WithMessage("این شماره موبایل قبلا در سیستم ثبت شده است");
        }

        //private bool IsMobileExist(string mobile)
        //{
        //    return _userService.IsExistMobile(mobile);
        //}
    }
}
