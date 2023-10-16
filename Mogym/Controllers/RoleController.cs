using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;

namespace Mogym.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService=roleService;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                var roleRecords = await _roleService.GetAllRecord();
                return View(roleRecords);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }

            return View();

        }


        public async Task<IActionResult> ChangeStatus(int id)
        {
            try
            {
                await _roleService.ChangeStatus(id);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";

            }

            return RedirectToAction(nameof(Index));

        }

    }
}
