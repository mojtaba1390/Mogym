using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.TrainerAchievement;
using Mogym.Application.Records.TrainerPlanCost;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    [Authorize]
    public class TrainerPlanCostController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly ITrainerPlanCostService _trainerPlanCostService;

        public TrainerPlanCostController(IHttpContextAccessor accessor,ITrainerPlanCostService trainerPlanCostService)
        {
            _accessor = accessor;
            _trainerPlanCostService=trainerPlanCostService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = _accessor.GetUser();
                var costs = await _trainerPlanCostService.GetListByUserId(userId);
                return View(costs);
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
        public async Task<IActionResult> Create(CreateTrainerCostsRecord model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var userId = _accessor.GetUser();
                    await _trainerPlanCostService.Create(userId, model);
                    return RedirectToAction(nameof(Index));
                }

                TempData["errormessage"] = Helper.GetModelSateErroMessage(ModelState);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }

            return View("NotFound");

        }
    }
}
