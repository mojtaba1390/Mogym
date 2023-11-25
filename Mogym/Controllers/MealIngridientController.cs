using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.ExerciseSet;
using Mogym.Application.Records.MealIngridient;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    [Authorize ]
    public class MealIngridientController : Controller
    {
        private readonly IMealIngridientService _mealIngridientService;
        private readonly IIngridientService _ingridientService;
        public MealIngridientController(IMealIngridientService mealIngridientService,IIngridientService ingridientService)
        {
            _mealIngridientService= mealIngridientService;
            _ingridientService = ingridientService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> GetMealIngridient(int mealId)
        {
            try
            {
                ViewBag.MealId=mealId;
                var mealIngridients = await _mealIngridientService.GetMealIngridients(mealId);

                var ingridients = await _ingridientService.GetAll();

                ViewData["Ingridients"] = new SelectList(ingridients, "Id", "Title");


                return PartialView("_MealIngridientDetail", mealIngridients);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");
        }


        [HttpPost]
        public async Task<IActionResult> AddMealIngridientRow(int counter, int mealId)
        {
            var ingridients = await _ingridientService.GetAll();

            ViewData["Ingridients"] = new SelectList(ingridients, "Id", "Title");
            return PartialView("_MealIngridientRow", new Tuple<int, int>(counter, mealId));
        }


        [HttpPost]
        public IActionResult AddMealIngridients(List<MealIngridientRecord> mealIngridientRecords)
        {
            try
            {
                _mealIngridientService.AddOrUpdateSets(mealIngridientRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

    }
}
