using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Plan;
using Mogym.Application.Records.Workout;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    [Authorize]
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;
        private readonly IExerciseVideoService _exerciseVideoService;
        private readonly IExerciseservice _exerciseservice;
        public WorkoutController(IWorkoutService workoutService, IExerciseVideoService exerciseVideoService,IExerciseservice exerciseservice)
        {
            _workoutService = workoutService;
            _exerciseVideoService = exerciseVideoService;
            _exerciseservice = exerciseservice;
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


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                bool isAnyExerciseExist = await _exerciseservice.IsAnyExcerciseExistByWorkoutId(id);
                return PartialView("_DeleteConfirmation",new Tuple<bool, int, string>(isAnyExerciseExist,id,"Workout"));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int deleteId)
        {
            try
            {

                 await _workoutService.Delete(deleteId);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

        public async Task<IActionResult> SentWorkoutDetail(int planId)
        {
            try
            {
                var sentWorkoutDetail = await _workoutService.GetSentWorkoutDetail(planId);
                return View();
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }
    }
}
