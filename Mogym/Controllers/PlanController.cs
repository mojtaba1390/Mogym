using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Profile;
using Mogym.Common;

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
        public async Task<IActionResult> MyUnPaidPlans()
        {
            try
            {
                var planRecords = await _planService.MyUnPaidPlans();
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
                bool isThisPlanStatusIsRegistered =
                    await _planService.IsThereAnyPlanWithStatus(Int32.Parse(planId),  EnumPlanStatus.Registered);
                if (!isThisPlanStatusIsRegistered)
                {
                    TempData["errormessage"] = "وضعیت برنامه درخواست داده شده پرداخت شده نمی باشد";
                    return View("IlegalRequest");
                }



                if (PaidPicture is not null)
                {
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "PaidPic", PaidPicture.FileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await PaidPicture.CopyToAsync(stream);
                        stream.Close();
                    }

                    await _planService.UpdatePaidPicture(Int32.Parse(planId),PaidPicture.FileName);
                    return RedirectToAction(nameof(MyUnPaidPlans));


                }
                TempData["errormessage"] = "تصویری بارگزاری نشده است";
                return RedirectToAction(nameof(MyUnPaidPlans));
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
                return View("IlegalRequest");
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }


        public async Task<IActionResult> PaidPlans()
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
        public async Task<IActionResult> MyPaidPlans()
        {
            try
            {
                var planRecords = await _planService.GetMyPaidPlans();
                return View(planRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }
        public async Task<IActionResult> MyApprovedPlans()
        {
            try
            {
                var planRecords = await _planService.MyApprovedPlans();
                return View(planRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }
        public async Task<IActionResult> MyRecivedPlans()
        {
            try
            {
                var planRecords = await _planService.MyRecivedPlans();
                return View(planRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }






        public async Task<IActionResult> ApprovePlan(int planId)
        {
            try
            {
                bool isThisPlanForThisTrainer = await _planService.IsThisPlanIdForThisTrainer(planId);
                if (!isThisPlanForThisTrainer)
                {
                    TempData["errormessage"] = "برنامه درخواست شده مربوط به شما نمی باشد";
                    return View("IlegalRequest");
                }

                bool isThisPlanStatusIsApproved =
                    await _planService.IsThereAnyPlanWithStatus(planId, EnumPlanStatus.Paid);
                if (!isThisPlanStatusIsApproved)
                {
                    TempData["errormessage"] = "وضعیت برنامه درخواست داده شده پرداخت شده نمی باشد";
                    return View("IlegalRequest");
                }

                await _planService.ApprovePlan(planId);
                 return RedirectToAction(nameof(ApprovePlans));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }

        public async Task<IActionResult> ApprovePlans()
        {
            try
            {
                var planRecords = await _planService.GetApprovePlans();
                return View(planRecords);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }


        public async Task<IActionResult> PlanDetails(int planId)
        {
            try
            {
                bool isThisPlanForThisTrainer = await _planService.IsThisPlanIdForThisTrainer(planId);
                if (!isThisPlanForThisTrainer)
                {
                    TempData["errormessage"] = "برنامه درخواست شده مربوط به شما نمی باشد";
                    return View("IlegalRequest");
                }

                bool isThisPlanStatusIsApproved =
                    await _planService.IsThereAnyPlanWithStatus(planId, EnumPlanStatus.TrainerApprovment);
                if (!isThisPlanStatusIsApproved)
                {
                    TempData["errormessage"] = "وضعیت برنامه درخواست داده شده تائید مربی نمی باشد";
                    return View("IlegalRequest");
                }




                var planDetails = await _planService.GetPlanDetails(planId);
                return View(planDetails);
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> AddDescription(int planId,string description)
        {
            try
            {
                bool isThisPlanForThisTrainer = await _planService.IsThisPlanIdForThisTrainer(planId);
                if (!isThisPlanForThisTrainer)
                {
                    TempData["errormessage"] = "برنامه درخواست شده مربوط به شما نمی باشد";
                    return View("IlegalRequest");
                }

                bool isThisPlanStatusIsApproved =
                    await _planService.IsThereAnyPlanWithStatus(planId, EnumPlanStatus.TrainerApprovment);
                if (!isThisPlanStatusIsApproved)
                {
                    TempData["errormessage"] = "وضعیت برنامه درخواست داده شده تائید مربی نمی باشد";
                    return View("IlegalRequest");
                }

                await _planService.AddDescription(planId,description);
                
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");
        }



        public async Task<IActionResult> SendPlan(int planId)
        {
            try
            {
                bool isThisPlanForThisTrainer = await _planService.IsThisPlanIdForThisTrainer(planId);
                if (!isThisPlanForThisTrainer)
                {
                    TempData["errormessage"] = "برنامه درخواست شده مربوط به شما نمی باشد";
                    return View("IlegalRequest");
                }

                bool isThisPlanStatusIsApproved =
                    await _planService.IsThereAnyPlanWithStatus(planId, EnumPlanStatus.TrainerApprovment);
                if (!isThisPlanStatusIsApproved)
                {
                    TempData["errormessage"] = "وضعیت برنامه درخواست شده تائید مربی نمی باشد";
                    return View("IlegalRequest");
                }

                await _planService.SendPlan(planId);
                return RedirectToAction(nameof(SentPlans));
            }
            catch (Exception e)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";

            }
            return View("NotFound");

        }

        public async Task<IActionResult> SentPlans()
        {
            try
            {
                var planRecords = await _planService.GetSentPlans();
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
