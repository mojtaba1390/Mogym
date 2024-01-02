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
using Mogym.Application.Interfaces.ICache;
using StackExchange.Redis;
using System.ComponentModel;
using Mogym.Common.ModelExtended;
using Telegram.Bot.Types;
using Message = Mogym.Common.ModelExtended.Message;

namespace Mogym.Controllers
{
    [DisplayName("اکانت")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailSender _emailSender;
        public AccountController(IUserService userService,
            IMenuService menuService, 
            IRedisCacheService redisCacheService,
            IConfiguration configuration, IHttpContextAccessor accessor, IEmailSender emailSender)
        {
            _userService = userService;
            _menuService = menuService;
            _redisCacheService = redisCacheService;
            _configuration = configuration;
            _accessor = accessor;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Login()
        {
            var isLogined = HttpContext.User.Identity.IsAuthenticated;
            if (isLogined)
                return RedirectToAction("Index", "Account");


            string returnUrl = HttpContext.Request.Query["returnUrl"];
            ViewData["returnUrl"] = returnUrl;
            return PartialView();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginRecord loginRecord)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var confirmSmsCode = await _userService.LoginAsync(loginRecord);

        //            ArgumentNullException.ThrowIfNull(confirmSmsCode);

        //            return RedirectToAction(nameof(confirmSmsCode), new { loginRecord.Mobile,loginRecord.ReturnUrl });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
        //    }


        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> Login(LoginRecord loginRecord, string? returnurl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.Login(loginRecord);
                    if (user != null)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName ??""),
                            new Claim(ClaimTypes.GivenName, (user.FirstName + " "+user.LastName) ?? ""),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.Roles.First().EnglishName),
                            new Claim(type: "ProfilePic", value: user.ProfilePic??"")

                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var peraperties = new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddMinutes(30)

                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, peraperties);

                        if (!string.IsNullOrWhiteSpace(returnurl) && Url.IsLocalUrl(returnurl))
                            return Redirect(returnurl);


                        return RedirectToAction(nameof(Index));
                    }


                    ViewBag.ErrorMessage = "کاربری با این مشخصات یافت نشد";

                }




            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }


            return View();
        }

        public async Task<IActionResult> ConfirmSmsCode(string mobile,string returnUrl)
        {
            ViewBag.Mobile = mobile;
            ViewData["returnUrl"] = returnUrl;
            return View();
        }



        /// <summary>
        /// اگه کد ارسالی و موبایل درست باشه، اطلاعات کاربر و نقش و دسترسی و منو های با وضعیت فعال گرفته میشه و در ردیس ذخیره میشه
        /// و بعد از اون، یه سری از اطلاعات برای کوکی ذخیره میشه
        /// </summary>
        /// <param name="confirmRegisterRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ConfirmSmsCode(ConfirmSmsRecord confirmRegisterRecord, string? returnurl)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = await _userService.GetUserWithRoleAndPermission(confirmRegisterRecord.Mobile);
                    //var activeMenus =await _menuService.GetActiveUserMenus();

                    //var userInfoKey = _configuration.GetSection("RedisKey").GetValue<string>("UserInformation");
                    //var activeMenusKey = _configuration.GetSection("RedisKey").GetValue<string>("MenuList");



                    //TODO: اینجا باید اول کلید های مربوطه تو ردیس پاک بشه بعد ست بشه

                    #region set redis cache
                    //var isRedisConnected = (await ConnectionMultiplexer.ConnectAsync(_configuration.GetConnectionString("RedisConnection") ?? string.Empty)).IsConnected; 
                    //if (isRedisConnected)
                    //{
                    //    await _redisCacheService.Set(userInfoKey, user, DateTime.Now.AddDays(1).Minute,
                    //        DateTime.Now.AddHours(1).Minute);
                    //    await _redisCacheService.Set(activeMenusKey, activeMenus, DateTime.Now.AddDays(1).Minute,
                    //        DateTime.Now.AddHours(1).Minute);
                    //}

                    #endregion


                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName ??""),
                        new Claim(ClaimTypes.GivenName, (user.FirstName + " "+user.LastName) ?? ""),
                        new Claim(ClaimTypes.MobilePhone, user.Mobile),
                        new Claim(ClaimTypes.Role, user.Roles.First().EnglishName)

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var peraperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, peraperties);


                    if (!string.IsNullOrWhiteSpace(returnurl) && Url.IsLocalUrl(returnurl))
                        return Redirect(returnurl);


                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }

            return RedirectToAction(nameof(ConfirmSmsCode),new{ confirmRegisterRecord .Mobile});
        }


        [Authorize]
        [DisplayName("خروج")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return View(nameof(Login));
        }


        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> RegisterTrainer()
        {
            return View("UnderConstruction");

        }


        public async Task<IActionResult> SignUp()
        {
            return View();
        }



        public async Task<IActionResult> SignUpTrainer()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SignUpTrainer(SignUpTrainerRecordNew signUpTrainerRecordNew)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.CreateTrainer(signUpTrainerRecordNew);

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName ??""),
                        new Claim(ClaimTypes.GivenName, (user.FirstName + " "+user.LastName) ?? ""),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Roles.First().EnglishName),
                        new Claim(type: "ProfilePic", value: user.ProfilePic??"")

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var peraperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, peraperties);

                    var message = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                        " ثبت نام جدید مربی", signUpTrainerRecordNew.FirstName + " " + signUpTrainerRecordNew.LastName + "-" + signUpTrainerRecordNew.Email + "-" + signUpTrainerRecordNew.Mobile);

                    await _emailSender.SendEmailAsync(message);


                    return RedirectToAction(nameof(Index));


                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
                return View("NotFound");
            }

            return View();

        }








        [HttpPost]
        public async Task<IActionResult> SignUp(SignupRecord signupRecord)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var user = await _userService.SignUp(signupRecord);
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName ??""),
                        new Claim(ClaimTypes.GivenName, (user.FirstName + " "+user.LastName) ?? ""),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Roles.First().EnglishName),
                        new Claim(type: "ProfilePic", value: user.ProfilePic??"")

                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var peraperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, peraperties);

                    var message = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
                        "ثبت نام جدید",signupRecord.FirstName + " " + signupRecord.LastName +"-"+signupRecord.Email );

                    await _emailSender.SendEmailAsync(message);


                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
                return View("NotFound");
            }

            return View();
        }


        [Authorize]
        [DisplayName("تغییر پسورد")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string passsword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(passsword))
                    ArgumentNullException.ThrowIfNull(passsword);

                await _userService.ChangePassword(passsword);
                await HttpContext.SignOutAsync();
                return View(nameof(Login));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }




    }
}
