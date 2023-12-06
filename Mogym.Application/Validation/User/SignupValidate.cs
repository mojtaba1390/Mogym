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

    public class SignupValidate : AbstractValidator<SignupRecord>
    {
        private readonly IUserService _userService;

        public SignupValidate(IUserService userService)
        {

            #region Dependency

            _userService = userService;


            #endregion


            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("نام را وارد کنید")
                .NotNull().WithMessage("نام را وارد کنید");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("نام خانوادگی را وارد کنید")
                .NotNull().WithMessage("نام خانوادگی را وارد کنید");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("ایمیل را وارد کنید")
                .NotNull().WithMessage("ایمیل را وارد کنید")
                .EmailAddress().WithMessage("فرمت ایمیل اشتباه می باشد")
                .Must(x => !UniqueEmail(x)).WithMessage("این آدرس ایمیل قبلا در سیستم ثبت شده است");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("پسورد را وارد کنید")
                .NotNull().WithMessage("پسورد را وارد کنید");

        }

        private bool UniqueEmail(string email)
        {
            return _userService.IsThereAnyEmailAddress(email);
        }
    }
}
