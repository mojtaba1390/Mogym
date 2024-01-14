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
    public class SignUpTrainerRecordNewValidate : AbstractValidator<SignUpTrainerRecordNew>
    {
        private readonly IUserService _userService;

        public SignUpTrainerRecordNewValidate(IUserService userService)
        {
            #region Dependency

            _userService = userService;


            #endregion

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("شماره موبایل نباید خالی باشد")
                .NotEmpty().WithMessage("شماره موبایل نباید خالی باشد")
                .Length(11).WithMessage("شماره موبایل باید ۱۱ رقم باشد")
                .Matches("^09\\d{9}$").WithMessage("فرمت شماره موبایل اشتباه می باشد")
                .Must(x => !IsMobileExist(x)).WithMessage("این شماره موبایل قبلا در سیستم ثبت شده است");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("نام را وارد کنید")
                .NotNull().WithMessage("نام را وارد کنید");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("نام خانوادگی را وارد کنید")
                .NotNull().WithMessage("نام خانوادگی را وارد کنید");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("پسورد را وارد کنید")
                .NotNull().WithMessage("پسورد را وارد کنید");

            RuleFor(x => x.IsConfirm).NotEqual(false).WithMessage("تیک موافقت با قوانین و مقررات را بزنید");
        }

        private bool IsMobileExist(string mobile)
        {
            return _userService.IsExistMobile(mobile);
        }
    }

}
