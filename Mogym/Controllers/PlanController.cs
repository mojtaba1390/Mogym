using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Profile;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("برنامه")]
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PlanController(IPlanService planService, IWebHostEnvironment webHostEnvironment)
        {
            _planService = planService;
            _webHostEnvironment = webHostEnvironment;
        }

        [DisplayName("برنامه های من")]
        public async Task<IActionResult> MyPlan()
        {
            try
            {
                var planRecords = await _planService.GetMyPlans();
                return View(planRecords);

            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }





        [HttpPost]
        public async Task<IActionResult> AddPaidPicture(string planId,IFormFile PaidPicture)
        {
            try
            {
                if (PaidPicture is not null)
                {
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "PaidPic", PaidPicture.FileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await PaidPicture.CopyToAsync(stream);
                        stream.Close();
                    }

                    await _planService.UpdatePaidPicture(Int32.Parse(planId),PaidPicture.FileName);
                    return RedirectToAction(nameof(MyPlan));


                }
                TempData["errormessage"] = "تصویری بارگزاری نشده است";
                return RedirectToAction(nameof(MyPlan));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");


        }


        public async Task<IActionResult> GetPlanAnswerQuestion(int planId)
        {
            try
            {
                var answerQuestion = await _planService.GetAnswerQuestionWithPlanId(planId);
                if (answerQuestion is not null)
                {
                    return PartialView("_ViewAQ", answerQuestion);

                }
                TempData["errormessage"] = "برنامه ی انتخاب شده مربوط به شما نمی باشد";
                return RedirectToAction(nameof(MyPlan));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }


        public async Task<IActionResult> PaidPlan()
        {
            try
            {
                var planRecords = await _planService.GetPaidPlans();
                return View(planRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }

    }
}
