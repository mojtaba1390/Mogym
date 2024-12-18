﻿using FluentValidation;
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
    public class LoginValidate : AbstractValidator<LoginRecord>
    {
        private readonly IUserService _userService;
        public LoginValidate(IUserService userService)
        {
            #region Dependency

            _userService = userService;


            #endregion


            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("شماره موبایل نباید خالی باشد")
                .NotEmpty().WithMessage("شماره موبایل نباید خالی باشد")
                .Length(11).WithMessage("شماره موبایل باید ۱۱ رقم باشد")
                .Matches("^09\\d{9}$").WithMessage("فرمت شماره موبایل اشتباه می باشد");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("پسورد نباید خالی باشد")
                .NotEmpty().WithMessage("پسورد نباید خالی باشد");
            //.DependentRules(() =>
            //{
            //    RuleFor(x => x.Mobile)
            //        .Must(mobile => !IsExistMobile(mobile))
            //        .WithMessage("این شماره موبایل قبلا ثبت شده است");
            //});






        }


    }







}
