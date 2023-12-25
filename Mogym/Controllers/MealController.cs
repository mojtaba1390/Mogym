using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Meal;
using Mogym.Application.Records.Workout;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    [Authorize]
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IMealIngridientService _mealIngridientService;
        private readonly IPlanService _planService;
        public MealController(IMealService mealService, IMealIngridientService mealIngridientService, IPlanService planService)
        {
            _mealService = mealService;
            _mealIngridientService = mealIngridientService;
            _planService = planService;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddMealRow(int counter, int planId)
        {
            return PartialView("_MealRow", new Tuple<int, int>(counter, planId));
        }


        public async Task<IActionResult> AddMeal(List<MealRecord> mealRecords)
        {
            try
            {
                await _mealService.AddOrUpdate(mealRecords);
                return RedirectToAction("PlanDetails", "Plan", new { planId = mealRecords.FirstOrDefault().PlanId });
            }
            catch (Exception ex)
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
                 await _mealService.Edit(id, title);
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id, int planId)
        {
            try
            {
                bool isThisWorkoutForThisTrainer = await _planService.IsThisPlanIdForThisTrainer(planId);
                if (!isThisWorkoutForThisTrainer)
                {
                    TempData["errormessage"] = "برنامه درخواست شده مربوط به شما نمی باشد";
                    return View("IlegalRequest");
                }

                bool isAnyIngridientExist = await _mealIngridientService.IsAnyIngridientExistByMealId(id);
                return PartialView("_DeleteConfirmation", new Tuple<bool, int,int, string>(isAnyIngridientExist, id,planId, "Meal"));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int deleteId, int planId)
        {
            try
            {

                await _mealService.Delete(deleteId);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return RedirectToAction("PlanDetails", "Plan", new { planId = planId });
        }


        public async Task<IActionResult> SentDietDetail(int planId)
        {
            try
            {
                var sentDietDetail = await _mealService.GetSentDietDetail(planId);
                return View(sentDietDetail);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }



    }
}
