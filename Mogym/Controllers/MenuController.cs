using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Menu;
using Mogym.Application.Records.Permission;

namespace Mogym.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService= menuService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMenuRecord model)
        {
            return View();
        }


        /// <summary>
        /// اگه زمانی که منو قراره ایجاد بشه، گزینه آیا والد دارد بله باشد، همه ی دسترسی ها برای مشخص کردن والد منو در دسترسی فراخوانی می شود
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetAllPermissionListForCreateMenuParent()
        {
            try
            {
                var permissions = await  _menuService.GetAllPermissionListForCreateMenuParent();
                return PartialView("_PermissionListForMenuPermissionParentPartialView", permissions);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }



    }
}
