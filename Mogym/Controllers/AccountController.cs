using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.User;

namespace Mogym.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISeriLogService _seriLogService;
        public AccountController(IUserService userService,ISeriLogService seriLogService)
        {
            _userService = userService;
            _seriLogService = seriLogService;
        }

        public async Task<IActionResult> RegisterUser()
        {
            _seriLogService.LogInformation("test serilog get");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserRecord registerUser)
        {

            _seriLogService.LogInformation("test serilog set");
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    errors = ModelState.Keys.SelectMany(k => ModelState[k].Errors)
                        .Select(m => m.ErrorMessage).ToArray()
                });
            }
            return View();
        }

    }
}
