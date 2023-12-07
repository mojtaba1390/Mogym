using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Mogym.Application.Interfaces;

namespace Mogym.Controllers
{
    [Authorize]
    public class RolePermissionController : Controller
    {
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        public RolePermissionController(IRolePermissionService rolePermissionService, IRoleService roleService, IPermissionService permissionService)
        {
            _rolePermissionService = rolePermissionService;
            _roleService = roleService;
            _permissionService = permissionService;
        }

        [DisplayName("سطح دسترسی")]
        public async Task<IActionResult> Index()
        {
            var rolePermissionRecords = await _permissionService.GetAllForRolePermission();
            ViewData["Roles"] = await _roleService.GetAllRecord();

            return View(rolePermissionRecords);
        }


        public async Task<IActionResult> DeleteByPermissionIdAndRoleId(int permissionId, int roleId)
        {
            await _rolePermissionService.DeleteByPermissionIdAndRoleId(permissionId, roleId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> AddByPermissionIdAndRoleId(int permissionId, int roleId)
        {
            await _rolePermissionService.AddByPermissionIdAndRoleId(permissionId, roleId);

            return RedirectToAction(nameof(Index));
        }
    }
}
