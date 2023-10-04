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

        public async Task<IActionResult> RegisterUser()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserRecord registerUser)
        {

            if (ModelState.IsValid)
            {
               var user=await _userService.AddAsync(registerUser);
            }
            return View();
        }

    }
}
