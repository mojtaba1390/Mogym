using System.Reflection.Metadata.Ecma335;
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
        public async Task<IActionResult> Login(LoginRecord loginRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var confirmSmsCode= await _userService.LoginAsync(loginRecord);

                    ArgumentNullException.ThrowIfNull(confirmSmsCode);

                     return RedirectToAction("ConfirmSmsCode", new { loginRecord.Mobile });
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }


            return View();
        }

        public async Task<IActionResult> ConfirmSmsCode(string mobile)
        {
            ViewBag.Mobile = mobile;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmSmsCode(ConfirmSmsRecord confirmRegisterRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {


                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }

            return null;
        }
        
    }
}
