using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Profile;
using Mogym.Application.Services;

namespace Mogym.Application.Validation.User
{
    public class SignUpTrainerValidate:AbstractValidator<SignUpTrainerRecord>
    {
        private readonly IUserService _userService;
        private readonly ITrainerProfileService _trainerProfileService;

        public SignUpTrainerValidate(IUserService userService, ITrainerProfileService trainerProfileService)
        {
            #region Dependency

            _userService = userService;
            _trainerProfileService = trainerProfileService;


            #endregion

            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("شماره موبایل نباید خالی باشد")
                .NotEmpty().WithMessage("شماره موبایل نباید خالی باشد")
                .Length(11).WithMessage("شماره موبایل باید ۱۱ رقم باشد")
                .Matches("^09\\d{9}$").WithMessage("فرمت شماره موبایل اشتباه می باشد")
                .Must(x=>IsMobileExist(x)).WithMessage("این شماره موبایل قبلا در سیستم ثبت نشده است");


            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("نام نباید خالی باشد")
                .NotNull().WithMessage("نام نباید خالی باشد");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("نام خانوادگی نباید خالی باشد")
                .NotNull().WithMessage("نام خانوادگی نباید خالی باشد");


            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("نام کاربری نباید خالی باشد")
                .NotNull().WithMessage("نام کاربری نباید خالی باشد")
                .Must(z => !IsDigitOrLetter(z)).WithMessage("نام کاربری فقط شامل حروف یا عدد باشد")
                .Must(z => !IsAnyUserNameExist(z)).WithMessage("نام کاربری انتخابی قبلا در سیستم ثبت شده است");

            RuleFor(x => x.Gender)
                .NotEqual(0)
                .WithMessage("جنسیت را انتخاب کنید");

            RuleFor(x => x.IsConfirm)
                .NotEqual(false)
                .WithMessage("تیک قوانین و مقررات را بزنید");

            RuleFor(x=>x.NationalCartPic)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("تصویر کارت ملی را وارد کنید")
                .NotNull().WithMessage("تصویر کارت ملی را وارد کنید");


            RuleFor(x=>x.TrainingCertificatePic)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("تصویر مدرک مربیگری را وارد کنید")
                .NotNull().WithMessage("تصویر مدرک مربیگری را وارد کنید");
        }

        private bool IsDigitOrLetter(string? username)
        {
            return username.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private bool IsMobileExist(string mobile)
        {
            return _userService.IsExistMobile(mobile);
        }

        private bool IsAnyUserNameExist(string? username)
        {
            return _trainerProfileService.IsAnyUserNameExist(username);
        }
    }
}
