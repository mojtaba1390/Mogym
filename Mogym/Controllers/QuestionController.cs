using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Question;
using Mogym.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Mogym.Application.Records.Profile;
using Mogym.Common.ModelExtended;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using Mogym.Application.Records.Plan;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("پرسشنامه")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITrainerProfileService _trainerProfileService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPlanService _planService;

        public QuestionController(ITrainerProfileService trainerProfileService, IQuestionService questionService, IHttpContextAccessor accessor, IWebHostEnvironment webHostEnvironment,IPlanService planService)
        {
            _trainerProfileService = trainerProfileService;
            _questionService = questionService;
            _accessor = accessor;
            _webHostEnvironment = webHostEnvironment;
            _planService = planService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AnswerQuestion(int trainerId)
        {
            try
            {
                var role = _accessor.GetCurrentUserRoleName();
                if (role.Equals("Trainer"))
                {
                    TempData["errormessage"] = "نقش شما مربی می باشد و امکان ثبت برنامه برای این نقش مقدور نیست";
                    return RedirectToAction("Index", "Account");

                }
                var createQuestionRecord = await _trainerProfileService.GetTrainerForCreateQuestion(trainerId);
                if (createQuestionRecord is null)
                {
                    TempData["errormessage"] = "مربی یافت نشد";
                    return View("NotFound");
                }

                var isThereAnyPlanWithThisUserAndTrainerInPastMonth =
                    await _planService.IsThereAnyPlanWithThisUserAndTrainerInPastMonth(trainerId);
                if (isThereAnyPlanWithThisUserAndTrainerInPastMonth)
                {
                    TempData["errormessage"] = "شما در یک ماه گذشته درخواست برنامه ای از این مربی داشته اید و امکان ثبت مجدد وجود ندارد.";
                    return View("IlegalRequest");
                }


                //if (trainerId == 1071)
                //{
                //    TempData["errormessage"] = "در حال حاضر امکان ثبت درخواست وجود ندارد.";
                //    return View("IlegalRequest");
                //}



                return View(createQuestionRecord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> AnswerQuestion(CreateQuestionRecord createQuestionRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string[] permittedExtensions = { ".jpeg", ".jpg", ".png" };

                    var pics = typeof(CreateQuestionRecord).GetProperties()
                                    .Where(x => x.PropertyType == typeof(IFormFile))
                                    .Select(x => (IFormFile)x.GetValue(createQuestionRecord))
                                    .ToList();

                    var picsWithData = pics.Where(z => z != null);

                    foreach (var pic in picsWithData)
                    {
                        var ext = Path.GetExtension(pic.FileName).ToLowerInvariant();
                        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                        {
                            ViewData["errormessage"] = "فرمت تصاویر باید از نوع jpeg یا jpg یا png باشد";
                            return RedirectToAction(nameof(AnswerQuestion), new { trainerId = createQuestionRecord.TrainerId });
                        }
                    }

                    foreach (var item in picsWithData)
                    {

                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "BodyPic", item.FileName);

                        if (item.Length < 2097152)
                        {
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                                stream.Close();
                            }
                        }
                        else
                        {
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                                stream.Close();

                                // Compress the image
                                using (var image = SixLabors.ImageSharp.Image.Load(path))
                                {
                                    // Resize the image to reduce its dimensions
                                    //image.Mutate(x => x.Resize(800, 600));

                                    // Compress the image with a quality setting
                                    var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 70 };
                                    image.Save(path, encoder);
                                }
                            }
                        }



                    }

                   var waitForPayRecord= await _questionService.AddQuestion(createQuestionRecord);

                    //var confirmAnswerQuestion = await _trainerProfileService.GetConfirmAnswerQuestion(createQuestionRecord.TrainerId, createQuestionRecord.TrainerPlanId);


                    //return View("ConfirmAnswerQuestion", confirmAnswerQuestion);
                    return View("WaitForPay", waitForPayRecord);
                }

                ViewData["errormessage"] = Helper.GetModelSateErroMessage(ModelState);
                return RedirectToAction(nameof(AnswerQuestion), new { trainerId = createQuestionRecord.TrainerId });
            }
            catch (Exception e)
            {
                return View("NotFound");
            }
        }







        [AllowAnonymous]
        public async Task<IActionResult> AttendanceQuestionForm(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    TempData["errormessage"] = "کد خالی می باشد";
                    return View("IlegalRequest");
                }




                var question = await _questionService.GetQuestionWithCode(code);
                if (question is null)
                {
                    ViewBag.ErrorMessage = "فرم مورد نظر یافت نشد";
                    return View("NotFound");
                }

                var isLogined = HttpContext.User.Identity.IsAuthenticated;
                if (!isLogined)
                {
                    var mobileNumberForAttendenceClientLogin =
                        await _questionService.SendOtpLoginForAttendanceClient(code);
                    return RedirectToAction("ConfirmSmsCode", "Account",
                        new {mobile = mobileNumberForAttendenceClientLogin, returnUrl = $"/AttendanceClient/{code}"});
                }


                return View("AttendanceClient", question);

            }
            catch (Exception e)
            {
                TempData["errormessage"] = e.Message=="02" ? 
                    "وضعیت پرسشنامه شما تغییر کرده و امکان ثبت مجدد اطلاعات وجود ندارد" :
                    "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";


                return View("IlegalRequest");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AnswerAttendanceClientQuestion(CreateAttendanceClientQuestionRecord createAttendanceClientQuestionRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string[] permittedExtensions = { ".jpeg", ".jpg", ".png" };

                    var pics = typeof(CreateAttendanceClientQuestionRecord).GetProperties()
                                    .Where(x => x.PropertyType == typeof(IFormFile))
                                    .Select(x => (IFormFile)x.GetValue(createAttendanceClientQuestionRecord))
                                    .ToList();

                    var picsWithData = pics.Where(z => z != null);

                    foreach (var pic in picsWithData)
                    {
                        var ext = Path.GetExtension(pic.FileName).ToLowerInvariant();
                        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                        {
                            ViewData["errormessage"] = "فرمت تصاویر باید از نوع jpeg یا jpg یا png باشد";
                            return RedirectToAction(nameof(AttendanceQuestionForm), new { code = createAttendanceClientQuestionRecord.Code });
                        }
                    }

                    foreach (var item in picsWithData)
                    {

                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "BodyPic", item.FileName);

                        if (item.Length < 2097152)
                        {
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                                stream.Close();
                            }
                        }
                        else
                        {
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                                stream.Close();

                                // Compress the image
                                using (var image = SixLabors.ImageSharp.Image.Load(path))
                                {
                                    // Resize the image to reduce its dimensions
                                    //image.Mutate(x => x.Resize(800, 600));

                                    // Compress the image with a quality setting
                                    var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 70 };
                                    image.Save(path, encoder);
                                }
                            }
                        }



                    }

                    var waitForPayRecord = await _questionService.UpdateQuestion(createAttendanceClientQuestionRecord);

                    // var confirmAnswerQuestion = await _trainerProfileService.GetConfirmAnswerQuestion(createAttendanceClientQuestionRecord.TrainerId, createAttendanceClientQuestionRecord.TrainerPlanId);


                    //return View("ConfirmAnswerQuestionAnonymous", confirmAnswerQuestion);
                    return View("WaitForPay", waitForPayRecord);

                }

                ViewData["errormessage"] = Helper.GetModelSateErroMessage(ModelState);
                return RedirectToAction(nameof(AnswerQuestion), new { trainerId = createAttendanceClientQuestionRecord.TrainerId });
            }
            catch (Exception e)
            {
                return View("NotFound");
            }
        }

    }
}
