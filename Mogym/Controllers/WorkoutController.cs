using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Workout;

namespace Mogym.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly IWorkoutService _workoutService;
        public WorkoutController(IWorkoutService workoutService)
        {
            _workoutService= workoutService;
        }
        public IActionResult Index()
        {
            return View();
        }

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
    }
}
