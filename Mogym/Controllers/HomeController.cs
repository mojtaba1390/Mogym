using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using MailKit.Search;
using Mogym.Application.Records.Profile;

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
        public async Task<IActionResult> Trainers(int? page,string search,string sort)
        {
            ViewBag.Sort = sort;

            var trainers = await _trainerProfileService.GetAllTrainers(page,search,sort);


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
        public async  Task<IActionResult> TrainerPanel(string msg)
        {
            ViewBag.Message = msg;
            return View();
        }

        public async Task<IActionResult> Search(string searchText)
        {
            try
            {
                ViewBag.SearchText = searchText;
                var lstTrainers = await _trainerProfileService.Search(searchText);
                return View("Trainers", lstTrainers);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<IActionResult> Sort(string by)
        {
            Tuple<int, int, List<TrainersRecord>> model=new Tuple<int, int, List<TrainersRecord>>(0,0,new List<TrainersRecord>());
            ViewBag.Sort = by;
            if (by == "newest")
                model = await _trainerProfileService.SortByNewest();
            else if (by == "sentPlan")
                model = await _trainerProfileService.SortBySentPlan();

            return View("Trainers", model);
        }
        public async Task<IActionResult> SortBySentPlan()
        {
            ViewBag.Sort = "sentPlan" ;
            var lstTrainers = await _trainerProfileService.SortBySentPlan();
            return View("Trainers", lstTrainers);
        }

    }
}
