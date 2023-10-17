using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Menu;

namespace Mogym.Application.Validation.Menu
{
    public class CreateMenuRecordValidate:AbstractValidator<CreateMenuRecord>
    {
        private readonly IMenuService _menuService;
        private readonly IPermissionService _permissionService;
        public CreateMenuRecordValidate(IMenuService menuService,IPermissionService permissionService)
        {
            #region Dependency
            _menuService=menuService;
            _permissionService=permissionService;
            

            #endregion



            RuleFor(x => x.EnglishName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("نام انگلیسی نباید خالی باشد")
                .NotNull().WithMessage("نام انگلیسی نباید خالی باشد")
                .Must(IsMenuNameExist).WithMessage("با این نام  قبلا منو ثبت شده است")
                .Must(IsPermissionExist).WithMessage("با این نام  قبلا دسترسی ثبت شده است");

            RuleFor(x => x.PersianName)
                .NotEmpty().WithMessage("نام فارسی نباید خالی باشد")
                .NotNull().WithMessage("نام فارسی نباید خالی باشد");

        }

        private bool IsPermissionExist(string englishName)
        {
            var permission = _permissionService.GetPermissionByEnglishName(englishName).Result;
            if (permission is not null)
                return true;
            return false;
        }

        private bool IsMenuNameExist(string englishName)
        {
            var menu=  _menuService.GetMenuByEnglishName(englishName).Result;
            if(menu is not null)
                return true;

            return false;
        }
    }
}
