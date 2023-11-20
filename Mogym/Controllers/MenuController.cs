using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Menu;
using Mogym.Application.Records.Permission;
using System.ComponentModel;
using System.Reflection;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("منو")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService= menuService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var menus = await _menuService.GetAllWithRelated();
                return View(menus);

            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";

            }

            return null;
        }

        [DisplayName("ایجاد منو جدید")]
        public async Task<IActionResult> Create()
        {
            var menus =await _menuService.GetAllActiveMenuList();
            ViewData["ParentId"] = new SelectList(menus, "Id", "PersianName");


            Assembly asm = Assembly.GetExecutingAssembly();
            var controllerlist = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .Select(type => type.GetTypeInfo())
                .Select(x => new ControllerAndActionRecord
                {
                    Controller = ("/" + x.Name.ToLower()).Replace("controller", ""),
                    ControllerDisplayName = x.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                }).Where(x => x.ControllerDisplayName != null).ToList();


            var controlleractionlist = asm.GetTypes()
                .Where(type => typeof(Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new ControllerAndActionRecord
                {
                    Controller = ("/") + x.DeclaringType.Name.ToLower().Replace("controller", "") + "/" + x.Name.ToLower(),
                    ControllerDisplayName = x.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                }).ToList().Where(x => x.ControllerDisplayName != null).ToList();

            controllerlist.AddRange(controlleractionlist);

            ViewData["LinkId"] = new SelectList(controllerlist, "Controller", "ControllerDisplayName");


            return View();
        }

        [HttpPost]
        public  IActionResult Create(CreateMenuRecord model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _menuService.AddMenu(model);
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";

            }
            return View(model);

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


        
        public async Task<IActionResult> Edit(int id)
        {
            return View("UnderConstruction");
        }
        public async Task<IActionResult> Delete(int id)
        {
            return View("UnderConstruction");
        }

    }
}
