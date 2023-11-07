using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Mogym.Application.Records.User;

namespace Mogym.Application.Validation.User
{
    public class SignUpTrainerValidate:AbstractValidator<SignUpTrainerRecord>
    {
        public SignUpTrainerValidate()
        {
            RuleFor(x => x.Mobile)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("شماره موبایل نباید خالی باشد")
                .NotEmpty().WithMessage("شماره موبایل نباید خالی باشد")
                .Length(11).WithMessage("شماره موبایل باید ۱۱ رقم باشد")
                .Matches("^09\\d{9}$").WithMessage("فرمت شماره موبایل اشتباه می باشد");

            RuleFor(x => x.NationalCode)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("کد ملی نباید خالی باشد")
                .NotEmpty().WithMessage("کد ملی نباید خالی باشد")
                .Length(10).WithMessage("کد ملی باید 10 رقم باشد");


            RuleFor(x => x.BirthDay)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("تاریخ تولد نباید خالی باشد")
                .NotEmpty().WithMessage("تاریخ تولد نباید خالی باشد");
        }
    }
}
