using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using System.ComponentModel;

namespace Mogym.Controllers
{
    [DisplayName("خانه")]
    public class HomeController : Controller
    {
        private readonly ITrainerProfileService _trainerProfileService;


        public HomeController(ITrainerProfileService trainerProfileService)
        {
            _trainerProfileService= trainerProfileService;
        }
        public async Task<IActionResult> Index()
        {
            //var isLogined = HttpContext.User.Identity.IsAuthenticated;
            //if (isLogined)
            //    return RedirectToAction("Index", "Account");


            var lastTrainers =await _trainerProfileService.GetLastTrainersForHomepage();
            return View(lastTrainers);
        }
        public async Task<IActionResult> ContactUs()
        {

            return View();
        }

        [DisplayName("مربیان")]
        public async Task<IActionResult> Trainers()
        {
            var trainers = await _trainerProfileService.GetAllTrainers();


            return View(trainers);
        }
    }
}
