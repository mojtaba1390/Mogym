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
        public MealController(IMealService mealService)
        {
            _mealService=mealService;
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

    }
}
