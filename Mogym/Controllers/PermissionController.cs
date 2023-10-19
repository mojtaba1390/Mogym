using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Records.Permission;
using System.ComponentModel;
using Mogym.Application.Interfaces;
using Newtonsoft.Json;

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


        public async Task<IActionResult> Index()
        {
            try
            {
                List<TreeViewNodeRecord> nodes = new List<TreeViewNodeRecord>();

                var permissions = await _permissionService.GetAll();
                var parents = permissions.Where(x => x.ParentId is null);
                var childs= permissions.Where(x => x.ParentId is not null);
                foreach (var parent in parents)
                {
                    nodes.Add(new TreeViewNodeRecord { Id = parent.Id.ToString(), Parent = "#", Text = parent.PersianName });

                }
                foreach (var child in childs)
                {
                    nodes.Add(new TreeViewNodeRecord { Id = child.Id.ToString(), Parent = child.ParentName, Text = child.PersianName });

                }

                ViewBag.Json = JsonConvert.SerializeObject(nodes);



            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }




            return View();
        }




        [HttpPost]
        public IActionResult Index(string selectedItems)
        {
            List<TreeViewNodeRecord> items = JsonConvert.DeserializeObject<List<TreeViewNodeRecord>>(selectedItems);
            return RedirectToAction("Index");
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
