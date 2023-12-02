using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;

namespace Mogym.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITrainerProfileService _trainerProfileService;


        public HomeController(ITrainerProfileService trainerProfileService)
        {
            _trainerProfileService= trainerProfileService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> Trainers()
        {
            var trainers = await _trainerProfileService.GetAllTrainers();


            return View(trainers);
        }
    }
}
