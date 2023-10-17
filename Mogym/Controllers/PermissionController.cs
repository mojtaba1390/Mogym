using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Records.Permission;
using System.ComponentModel;
using Mogym.Application.Interfaces;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("دسترسی")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }


        public IActionResult Index()
        {
            return View();
        }



        public async Task<IActionResult> Create()
        {
            var permissions =await _permissionService.GetAll();
            var createPermissionRecord = new CreatePermissionRecord() {Permissions = permissions};
            return View(createPermissionRecord);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionRecord model)
        {
            return View();
        }


    }
}
