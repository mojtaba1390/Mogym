using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Records.User;
using Mogym.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

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

                     return RedirectToAction(nameof(confirmSmsCode), new { loginRecord.Mobile });
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

                    var user =await  _userService.GetUserWithRoleAndPermission(confirmRegisterRecord.Mobile);


                    //Todo:
                    // اینجا باید ردیس فراخوانی بشه و اطلاعات یوزر با رول و دسترسی با کلید بر اساس یونیک یوزرنیم ذخیره بشه
                    //همینطور اینجا باید منو از دیتابیس فراخوانی بشه و اونم در ردیس فراخوانی بشه
                    //فیلتر منو ها به صورت دیفالت بر اساس اکتیو هست و نیاز به شرط نداره، فقط تو لیست منو ها باید این شرط برداشته بشه
                    //کلید ها برای ردیس حتما تو appsetting تو لیست rediskey تعریف بشه و از اونجا خونده بشه
                    


                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.GivenName, (user.FirstName + " "+user.LastName) ?? "")

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var peraperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };


                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, peraperties);

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }

            return null;
        }


        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return View(nameof(Login));
        }



        public async Task<IActionResult> Index()
        {
            return View();
        }
        
    }
}
