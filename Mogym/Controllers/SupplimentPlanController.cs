using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.AutoMapper.SupplimentPlan;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.MealIngridient;

namespace Mogym.Controllers
{
    public class SupplimentPlanController : Controller
    {
        private readonly ISupplimentService _supplimentService;
        private readonly ISupplimentPlanService _supplimentPlanService;
        public SupplimentPlanController(ISupplimentService supplimentService, ISupplimentPlanService supplimentPlanService)
        {
            _supplimentService = supplimentService;
            _supplimentPlanService = supplimentPlanService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddSupplimentPlanRow(int counter, int planId)
        {
            var suppliments = await _supplimentService.GetAll();

            ViewData["Suppliments"] = new SelectList(suppliments, "Id", "Title");
            return PartialView("_SupplimentPlanRow", new Tuple<int, int>(counter, planId));
        }


        [HttpPost]
        public IActionResult AddSupplimentPlans(List<SupplimentPlanRecord> supplimentPlanRecords)
        {
            try
            {
                _supplimentPlanService.AddOrUpdate(supplimentPlanRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

    }
}
