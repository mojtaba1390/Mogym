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

namespace Mogym.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISeriLogService _logger;
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, ISeriLogService logger, IMenuService menuService, IRedisCacheService redisCacheService, IConfiguration configuration)
        {
            _userService = userService;
            _logger = logger;
            _menuService = menuService;
            _redisCacheService = redisCacheService;
            _configuration = configuration;
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
                    var confirmSmsCode = await _userService.LoginAsync(loginRecord);

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



        /// <summary>
        /// اگه کد ارسالی و موبایل درست باشه، اطلاعات کاربر و نقش و دسترسی و منو های با وضعیت فعال گرفته میشه و در ردیس ذخیره میشه
        /// و بعد از اون، یه سری از اطلاعات برای کوکی ذخیره میشه
        /// </summary>
        /// <param name="confirmRegisterRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ConfirmSmsCode(ConfirmSmsRecord confirmRegisterRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = await _userService.GetUserWithRoleAndPermission(confirmRegisterRecord.Mobile);
                    var activeMenus =await _menuService.GetAllActiveMenuList();

                    var userInfoKey = _configuration.GetSection("RedisKey").GetValue<string>("UserInformation");
                    var rolesKey = _configuration.GetSection("RedisKey").GetValue<string>("UserRoles");
                    var permissionsKey = _configuration.GetSection("RedisKey").GetValue<string>("UserPermissions");
                    var activeMenusKey = _configuration.GetSection("RedisKey").GetValue<string>("MenuList");



                    //TODO: اینجا باید اول کلید های مربوطه تو ردیس پاک بشه بعد ست بشه

                    #region set redis cache
                    await _redisCacheService.Set(userInfoKey, user, DateTime.Now.AddDays(1).Minute,
                        DateTime.Now.AddHours(1).Minute);
                    await _redisCacheService.Set(rolesKey, user.Roles, DateTime.Now.AddDays(1).Minute,
                         DateTime.Now.AddHours(1).Minute);
                    await _redisCacheService.Set(permissionsKey, user.Permissions, DateTime.Now.AddDays(1).Minute,
                         DateTime.Now.AddHours(1).Minute);
                    await _redisCacheService.Set(activeMenusKey, activeMenus, DateTime.Now.AddDays(1).Minute,
                         DateTime.Now.AddHours(1).Minute);
                    #endregion



                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName ??""),
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

            return RedirectToAction(nameof(ConfirmSmsCode),new{ confirmRegisterRecord .Mobile});
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
