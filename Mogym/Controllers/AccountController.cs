using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.User;

namespace Mogym.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISeriLogService _logger;
        private readonly IUserService _userService;
        public AccountController(IUserService userService, ISeriLogService logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<IActionResult> Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(RegisterUserRecord registerUser)
        {
            _logger.LogInformation("serilog worked");

            try
            {
                if (ModelState.IsValid)
                {
                     await _userService.AddAsync(registerUser);

                     var confirmRegister = new ConfirmRegisterRecord() {Mobile = registerUser.Mobile};
                     RedirectToAction("ConfirmRegister", new { confirmRegister });
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }


            return View();
        }

        public async Task<IActionResult> ConfirmRegister(ConfirmRegisterRecord confirmRegisterRecord)
        {
            return View(confirmRegisterRecord);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmRegister(string mobile)
        {
            return View(mobile);
        }

    }
}
