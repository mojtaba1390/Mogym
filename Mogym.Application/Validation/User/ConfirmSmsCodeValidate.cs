using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.User;

namespace Mogym.Application.Validation.User
{
    public class ConfirmSmsCodeValidate:AbstractValidator<ConfirmSmsRecord>
    {
        private readonly IUserService _userService;

        public ConfirmSmsCodeValidate(IUserService userService)
        {
            #region Dependency

            _userService = userService;


            #endregion


            RuleFor(x => x.ConfirmCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("کد تايید نباید خالی باشد")
                .NotNull().WithMessage("کد تايید نباید خالی باشد");


            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("موبایل  خالی می باشد")
                .NotNull().WithMessage("موبایل  خالی می باشد")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Mobile)
                        .Must(IsExistMobile).WithMessage("این شماره موبایل در سیستم ثبت نشده است");
                });

            RuleFor(x => new {x.Mobile, x.ConfirmCode})
                .Must(z=> IsExistMobileWithConfirmSmsCode(z.Mobile,z.ConfirmCode))
                .WithMessage("کد ارسالی وارد شده برای این شماره موبایل نمی باشد");

        }

        private bool IsExistMobileWithConfirmSmsCode(string mobile,string confirmSmsCode)
        {
            return _userService.IsExistMobileWithConfirmSmsCode(mobile, confirmSmsCode);
        }

        private bool IsExistMobile(string mobile)
        {
            return  _userService.IsExistMobile(mobile);
        }
    }
}
