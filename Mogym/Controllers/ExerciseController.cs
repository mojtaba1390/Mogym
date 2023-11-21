using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Workout;

namespace Mogym.Controllers
{
    public class ExerciseController : Controller
    {

        private readonly IWorkoutService _workoutService;
        private readonly IExerciseVideoService _exerciseVideoService;
        private readonly IExerciseservice _exerciseservice;
        public ExerciseController(IWorkoutService workoutService, IExerciseVideoService exerciseVideoService,IExerciseservice exerciseservice)
        {
            _workoutService = workoutService;
            _exerciseVideoService = exerciseVideoService;
            _exerciseservice = exerciseservice;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddExerciseRow(int counter, int workoutId)
        {
            var superSets = await _workoutService.GetSuperSetExercises(workoutId);
            var exerciseVideos = await _exerciseVideoService.GetAllExerciseVideo();

            ViewData["ExersiceVideo"] = new SelectList(exerciseVideos, "Id", "Title");
            ViewData["SuperSets"] = new SelectList(superSets, "Id", "Title");

            return PartialView("_ExerciseRow", new Tuple<int, int>(counter, workoutId));
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise(List<WorkoutExerciseRecord> workoutExerciseRecords)
        {
            try
            {
                await _exerciseservice.AddAndUpdateExercises(workoutExerciseRecords);

                return RedirectToAction("WorkoutDetail", "Workout",
                    new { id = workoutExerciseRecords.FirstOrDefault().WorkoutId});
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");

        }
    }
}
