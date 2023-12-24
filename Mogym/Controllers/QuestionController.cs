using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Question;
using Mogym.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Mogym.Application.Records.Profile;

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

        public QuestionController(ITrainerProfileService trainerProfileService, IQuestionService questionService, IHttpContextAccessor accessor, IWebHostEnvironment webHostEnvironment)
        {
            _trainerProfileService = trainerProfileService;
            _questionService = questionService;
            _accessor = accessor;
            _webHostEnvironment = webHostEnvironment;
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
                    var pics = typeof(CreateQuestionRecord).GetProperties()
                        .Where(x => x.PropertyType == typeof(IFormFile))
                        .Select(x => (IFormFile)x.GetValue(createQuestionRecord))
                        .ToList();

                    var picsWithData = pics.Where(z=>z!=null);


                    foreach (var item in picsWithData)
                    {

                            var path = Path.Combine(_webHostEnvironment.WebRootPath, "BodyPic", item.FileName);
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                                stream.Close();
                            }
                        

                    }

                    await _questionService.AddQuestion(createQuestionRecord);

                    var confirmAnswerQuestion = await _trainerProfileService.GetConfirmAnswerQuestion(createQuestionRecord.TrainerId,createQuestionRecord.TrainerPlanId);

                    return View("ConfirmAnswerQuestion", confirmAnswerQuestion);
                }

                ViewData["errormessage"] = Helper.GetModelSateErroMessage(ModelState);
                return RedirectToAction(nameof(AnswerQuestion), new { trainerId = createQuestionRecord.TrainerId });
            }
            catch (Exception e)
            {
                return View("NotFound");
            }
        }

    }
}
