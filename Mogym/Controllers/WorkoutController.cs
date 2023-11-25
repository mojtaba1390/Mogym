using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Workout;

namespace Mogym.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;
        private readonly IExerciseVideoService _exerciseVideoService;
        public WorkoutController(IWorkoutService workoutService, IExerciseVideoService exerciseVideoService)
        {
            _workoutService = workoutService;
            _exerciseVideoService = exerciseVideoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //TODO : اگه وقت شد این قسمت دو تا مورد رو از هم جدا کنم. چون ویرایش با اجکس داره انجام میشه،میشه اد هم همین کارو کرد
        public async Task<IActionResult> AddWorkout(List<WorkoutRecord> workoutRecords) 
        {
            try
            {
                await _workoutService.AddOrUpdate(workoutRecords);
                return RedirectToAction("PlanDetails", "Plan", new { planId = workoutRecords.FirstOrDefault().PlanId });
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");

        }


        [HttpPost]
        public async Task<IActionResult> AddWorkoutRow(int counter, int planId)
        {
            return PartialView("_WorkoutRow",new Tuple<int,int>(counter,planId));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, string title)
        {
            try
            {
                await _workoutService.Edit(id,title);
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");
        }
        public async Task<IActionResult> Delete(int id)
        {
            return RedirectToAction();
        }

        public async Task<IActionResult> WorkoutDetail(int id)
        {
            try
            {
                var workoutDetails = await _workoutService.GetWorkoutDetails(id);
                var superSets = await _workoutService.GetSuperSetExercises(id);
                var exerciseVideos = await _exerciseVideoService.GetAllExerciseVideo();

                ViewData["ExersiceVideo"] = new SelectList(exerciseVideos, "Id", "Title");
                ViewData["SuperSets"] = new SelectList(superSets, "Id", "Title");

                ViewBag.WorkoutId=id;

                return View(workoutDetails);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }
    }
}
