using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.ExerciseSet;
using Mogym.Application.Records.Workout;

namespace Mogym.Controllers
{
    public class ExerciseSetController : Controller
    {
        private readonly IExerciseSetService _exerciseSetService;
        private readonly IExerciseservice _exerciseservice;
        public ExerciseSetController(IExerciseSetService exerciseSetService, IExerciseservice exerciseservice)
        {
            _exerciseSetService = exerciseSetService;
            _exerciseservice = exerciseservice;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddExerciseSetRow(int counter, int exerciseId)
        {
            return PartialView("_ExerciseSetRow", new Tuple<int, int>(counter, exerciseId));
        }


        public async Task<IActionResult> ExerciseSetDetail(int exerciseId)
        {
            try
            {
                var exerciseSets = await _exerciseSetService.GetExerciseSetDetail(exerciseId);
                ViewBag.ExerciseId = exerciseId;

                return PartialView("_ExerciseSet", exerciseSets);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> AddExerciseSet(List<ExerciseSetRecord> exerciseSetRecords)
        {
            try
            {
                 _exerciseSetService.AddOrUpdateSets(exerciseSetRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }

            var exercise =await _exerciseservice.GetByIdAsync(exerciseSetRecords.First().ExerciseId);
            return RedirectToAction("WorkoutDetail", "Workout",new{id=exercise.WorkoutId});
        }
    }
}


