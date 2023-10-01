using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.User;

namespace Mogym.Application.Validation.User
{
    public class CreateTrainerValidate : AbstractValidator<CreateTrainerRecord>
    {
        public CreateTrainerValidate()
        {
            RuleFor(x => x.Mobile)
                .NotNull().WithMessage("شماره موبایل نباید خالی باشد")
                .NotEmpty().WithMessage("شماره موبایل نباید خالی باشد")
                .Length(11).WithMessage("شماره موبایل نباید باید ۱۱ رقم باشد")
                .Matches("^09\\d{9}$").WithMessage("فرمت شماره موبایل اشتباه می باشد");
            RuleFor(x => x.NationalCode)
                .NotNull().WithMessage("کد ملی نباید خالی باشد")
                .NotEmpty().WithMessage("کد ملی نباید خالی باشد")
                .Length(10).WithMessage("کد ملی نباید باید ۱۰ رقم باشد");
        }
    }
}
