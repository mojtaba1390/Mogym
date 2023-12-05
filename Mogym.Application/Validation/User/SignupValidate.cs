using FluentValidation;
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
        public SignupValidate()
        {
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
                .EmailAddress().WithMessage("فرمت ایمیل اشتباه می باشد");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("پسورد را وارد کنید")
                .NotNull().WithMessage("پسورد را وارد کنید");

        }
    }
}
