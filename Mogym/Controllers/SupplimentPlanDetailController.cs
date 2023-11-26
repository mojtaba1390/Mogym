using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.MealIngridient;
using Mogym.Application.Records.SupplimentPlanDetail;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    public class SupplimentPlanDetailController : Controller
    {
        private readonly ISupplimentPlanDetailService _supplimentPlanDetailService;
        private readonly ISupplimentService _supplimentService;
        public SupplimentPlanDetailController(ISupplimentPlanDetailService supplimentPlanDetailService, ISupplimentService supplimentService)
        {
            _supplimentPlanDetailService = supplimentPlanDetailService;
            _supplimentService = supplimentService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetSupplimentPlanDetail(int supplimentPlanId)
        {
            try
            {
                ViewBag.SupplimentPlanId = supplimentPlanId;
                var supplimentPlanDetails = await _supplimentPlanDetailService.GetSupplimentPlanDetail(supplimentPlanId);

                var suppliments = await _supplimentService.GetAll();

                ViewData["Suppliments"] = new SelectList(suppliments, "Id", "Title");


                return PartialView("_SupplimentPlanDetail", supplimentPlanDetails);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");
        }


        [HttpPost]
        public async Task<IActionResult> AddSupplimentPlanDetailRow(int counter, int supplimentPlanId)
        {
            var suppliments = await _supplimentService.GetAll();

            ViewData["Suppliments"] = new SelectList(suppliments, "Id", "Title");
            return PartialView("_SupplimentPlanDetailRow", new Tuple<int, int>(counter, supplimentPlanId));
        }

        [HttpPost]
        public IActionResult AddSupplimentPlanDetails(List<SupplimentPlanDetailRecord> supplimentPlanDetailRecords)
        {
            try
            {
                _supplimentPlanDetailService.AddOrUpdateSets(supplimentPlanDetailRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

    }
}
