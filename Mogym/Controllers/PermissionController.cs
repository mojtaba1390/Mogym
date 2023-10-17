using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Records.Permission;

namespace Mogym.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionRecord model)
        {
            return View();
        }


    }
}
