using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Mogym.Controllers
{
    [DisplayName("خانه")]
    public class HomeController : Controller
    {
        private readonly ITrainerProfileService _trainerProfileService;
        private readonly IHttpContextAccessor _accessor;

        public HomeController(ITrainerProfileService trainerProfileService, IHttpContextAccessor accessor)
        {
            _trainerProfileService = trainerProfileService;
            _accessor = accessor;
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

        [DisplayName("لینک صفحه من")]
        [Authorize]
        public async Task<IActionResult> MyPageLink()
        {
            return View();
        }
        public async  Task<IActionResult> Terms()
        {
            return View();
        }



    }
}
