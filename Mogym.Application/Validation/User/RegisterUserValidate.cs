using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.User;
using Microsoft.EntityFrameworkCore;

namespace Mogym.Application.Validation.User
{
    public class RegisterUserValidate : AbstractValidator<RegisterUserRecord>
    {
        private readonly IUserService _userService;
        public RegisterUserValidate(IUserService userService)
        {
            _userService = userService;
            RuleFor(x => x.Mobile)
                .NotNull().WithMessage("شماره موبایل نباید خالی باشد")
                .NotEmpty().WithMessage("شماره موبایل نباید خالی باشد")
                .Length(11).WithMessage("شماره موبایل نباید باید ۱۱ رقم باشد")
                .Matches("^09\\d{9}$").WithMessage("فرمت شماره موبایل اشتباه می باشد")
                .Must(IsExistMobile).WithMessage("این شماره موبایل قبلا ثبت شده است");
        }

        private bool IsExistMobile(RegisterUserRecord user,string mobile)
        {
           return  _userService.IsExistMobile(user.Mobile);
        }
    }







}
