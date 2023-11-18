using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Records.Workout;

namespace Mogym.Controllers
{
    public class WorkoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> AddWorkout(List<WorkoutRecord> workoutRecords)
        {
            return null;
        }


        [HttpPost]
        public async Task<IActionResult> AddWorkoutRow(int counter, int planId)
        {
            return PartialView("_WorkoutRow",planId);
        }
    }
}
