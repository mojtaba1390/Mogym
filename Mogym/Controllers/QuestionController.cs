using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Mogym.Application.Interfaces;
using Mogym.Infrastructure.Interfaces;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("پرسشنامه")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITrainerProfileService _trainerProfileService;
        public QuestionController(ITrainerProfileService trainerProfileService, IQuestionService questionService)
        {
            _trainerProfileService = trainerProfileService;
            _questionService = questionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AnswerQuestion(int trainerId)
        {
            var createQuestionRecord = await _trainerProfileService.GetTrainerForCreateQuestion(trainerId);
            if (createQuestionRecord is null)
            {
                ViewData["errormessage"] = "مربی یافت نشد";
                return View("NotFound");
            }

            return View(createQuestionRecord);
        }
        
    }
}
