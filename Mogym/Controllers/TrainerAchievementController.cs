using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.TrainerAchievement;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    public class TrainerAchievementController : Controller
    {
        private readonly ITrainerAchievementService _trainerAchievementService;
        private readonly IHttpContextAccessor _accessor;
        public TrainerAchievementController(ITrainerAchievementService trainerAchievementService, IHttpContextAccessor accessor)
        {
            _trainerAchievementService = trainerAchievementService;
            _accessor = accessor;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _accessor.GetUser();
                var achievements = await _trainerAchievementService.GetListByUserId(userId);
                return View(achievements);
            }
            catch (Exception a)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }

            return null;
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Create(CreateTrainerAchievementRecord model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var userId = _accessor.GetUser();
                    await _trainerAchievementService.Create(userId, model);
                    return RedirectToAction(nameof(Index));
                }

                TempData["errormessage"]=Helper.GetModelSateErroMessage(ModelState);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }

            return View("NotFound");

        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                     _trainerAchievementService.Delete(id);
                    return RedirectToAction(nameof(Index));
                }

                TempData["errormessage"]=Helper.GetModelSateErroMessage(ModelState);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }

            return View("NotFound");

        }


    }
}
