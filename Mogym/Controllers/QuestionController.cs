using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Question;
using Mogym.Infrastructure.Interfaces;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("پرسشنامه")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITrainerProfileService _trainerProfileService;
        private readonly IServiceProvider _serviceProvider;
        public QuestionController(ITrainerProfileService trainerProfileService, IQuestionService questionService,IServiceProvider serviceProvider)
        {
            _trainerProfileService = trainerProfileService;
            _questionService = questionService;
            _serviceProvider = serviceProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AnswerQuestion(int trainerId)
        {
            try
            {
                //var roles = Helper.GetCurrentUserRole(_serviceProvider);
                //if (roles.Any(x => x.Id == 3))
                //{
                //    TempData["errormessage"] = "نقش شما مربی می باشد و امکان ثبت برنامه برای این نقش مقدور نیست";
                //    return RedirectToAction("Index", "Account");

                //}
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
                    await _questionService.AddQuestion(createQuestionRecord);
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
