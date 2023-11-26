using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.SupplimentPlan;

namespace Mogym.Controllers
{
    [Authorize]
    public class SupplimentPlanController : Controller
    {
        private readonly ISupplimentPlanService _supplimentPlanService;
        public SupplimentPlanController(ISupplimentPlanService supplimentPlanService)
        {
            _supplimentPlanService = supplimentPlanService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddSupplimentPlanRow(int counter, int planId)
        {
            return PartialView("_SupplimentPlanRow", new Tuple<int, int>(counter, planId));
        }


        public async Task<IActionResult> AddSupplimentPlans(List<SupplimentPlanRecord> supplimentPlanRecords)
        {
            try
            {
               await _supplimentPlanService.AddOrUpdate(supplimentPlanRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

    }
}
