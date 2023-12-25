using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.SupplimentPlan;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    [Authorize]
    public class SupplimentPlanController : Controller
    {
        private readonly ISupplimentPlanService _supplimentPlanService;
        private readonly ISupplimentPlanDetailService _supplimentPlanDetailService;
        public SupplimentPlanController(ISupplimentPlanService supplimentPlanService, ISupplimentPlanDetailService supplimentPlanDetailService)
        {
            _supplimentPlanService = supplimentPlanService;
            _supplimentPlanDetailService = supplimentPlanDetailService;
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
               return RedirectToAction("PlanDetails", "Plan", new { planId = supplimentPlanRecords.FirstOrDefault().PlanId });

            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id,int planId)
        {
            try
            {

                bool isAnySupplimentDetail = await _supplimentPlanDetailService.IsAnySupplimentDetailExistBySupplimentPlanId(id);
                return PartialView("_DeleteConfirmation", new Tuple<bool, int,int, string>(isAnySupplimentDetail, id,planId, "SupplimentPlan"));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int deleteId,int planId)
        {
            try
            {

                await _supplimentPlanService.Delete(deleteId);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return RedirectToAction("PlanDetails", "Plan", new { planId = planId });
        }


        public async Task<IActionResult> SentSupplimentDetail(int planId)
        {
            try
            {
                var sentSupplimentDetail = await _supplimentPlanService.GetSentSupplimentDetail(planId);
                return View(sentSupplimentDetail);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, string title)
        {
            try
            {
                await _supplimentPlanService.Edit(id, title);
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");
        }

    }
}
