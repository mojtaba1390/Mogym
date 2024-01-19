﻿using System.Reflection.Metadata.Ecma335;
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
using Message = Mogym.Common.ModelExtended.Message;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Mogym.Application.Records.Question;
using SixLabors.ImageSharp;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Server;
using AutoMapper;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Reflection;

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
        private readonly ISmsService _smsService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(IUserService userService,
            IMenuService menuService, 
            IRedisCacheService redisCacheService,
            IConfiguration configuration, IHttpContextAccessor accessor, IEmailSender emailSender, ISmsService smsService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _menuService = menuService;
            _redisCacheService = redisCacheService;
            _configuration = configuration;
            _accessor = accessor;
            _emailSender = emailSender;
            _smsService = smsService;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> LoginMobile()
        {
            var isLogined = HttpContext.User.Identity.IsAuthenticated;
            if (isLogined)
                return RedirectToAction("Index", "Account");


            string returnUrl = HttpContext.Request.Query["returnUrl"];
            ViewData["returnUrl"] = returnUrl;
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Login(OTPLoginRecord otpLoginRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var confirmSmsCode = await _userService.LoginAsync(otpLoginRecord);

                    ArgumentNullException.ThrowIfNull(confirmSmsCode);

                    return RedirectToAction(nameof(confirmSmsCode), new { otpLoginRecord.Mobile, otpLoginRecord.ReturnUrl });
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LoginMobile(LoginRecord loginRecord, string? returnurl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.LoginMobile(loginRecord);
                    if (user is not null)
                    {
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName ??""),
                            new Claim(ClaimTypes.GivenName,!string.IsNullOrWhiteSpace(user.FirstName)? (user.FirstName + " "+user.LastName) : ""),
                            //new Claim(ClaimTypes.Email, user.Email??""),
                            new Claim(ClaimTypes.Role, user.Roles.First().EnglishName),
                            new Claim(type: "ProfilePic", value: user.ProfilePic??"")

                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var peraperties = new AuthenticationProperties()
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTime.UtcNow.AddDays(1)

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

        //[HttpPost]
        //public async Task<IActionResult> Login(ConfirmSmsRecord loginRecord, string? returnurl)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = await _userService.Login(loginRecord);

        //            if (user != null)
        //            {
        //                var claims = new List<Claim>()
        //                {
        //                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //                    new Claim(ClaimTypes.Name, user.UserName ??""),
        //                    new Claim(ClaimTypes.GivenName, (user.FirstName + " "+user.LastName) ?? ""),
        //                    new Claim(ClaimTypes.Email, user.Email),
        //                    new Claim(ClaimTypes.Role, user.Roles.First().EnglishName),
        //                    new Claim(type: "ProfilePic", value: user.ProfilePic??"")

        //                };
        //                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //                var principal = new ClaimsPrincipal(identity);

        //                var peraperties = new AuthenticationProperties()
        //                {
        //                    IsPersistent = true,
        //                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)

        //                };

        //                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, peraperties);

        //                if (!string.IsNullOrWhiteSpace(returnurl) && Url.IsLocalUrl(returnurl))
        //                    return Redirect(returnurl);


        //                return RedirectToAction(nameof(Index));
        //            }


        //            ViewBag.ErrorMessage = "کاربری با این مشخصات یافت نشد";

        //        }




        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
        //    }


        //    return View();
        //}

        public async Task<IActionResult> ConfirmSmsCode(string mobile,string returnUrl,string errormessage)
        {
            ViewBag.Mobile = mobile;
            ViewData["returnUrl"] = returnUrl;
            ViewData["ErrorMessage"] = errormessage;

            return View();
        }



        /// <summary>
        /// اگه کد ارسالی و موبایل درست باشه، اطلاعات کاربر و نقش و دسترسی و منو های با وضعیت فعال گرفته میشه و در ردیس ذخیره میشه
        /// و بعد از اون، یه سری از اطلاعات برای کوکی ذخیره میشه
        /// </summary>
        /// <param name="confirmRegisterRecord"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ConfirmSmsCode(OTPRecord confirmRegisterRecord, string? returnurl)
        {
            string message = string.Empty;
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
                        new Claim(ClaimTypes.GivenName,!string.IsNullOrWhiteSpace(user.FirstName)? (user.FirstName + " "+user.LastName) : ""),
                        //new Claim(ClaimTypes.Email, user.Email??""),
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


                    if (!string.IsNullOrWhiteSpace(returnurl) && Url.IsLocalUrl(returnurl))
                        return Redirect(returnurl);


                    return RedirectToAction(nameof(Index));
                }

                 message = Helper.GetModelSateErroMessage(ModelState);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }

            return RedirectToAction(nameof(ConfirmSmsCode),new{ mobile=confirmRegisterRecord .Mobile,returnurl=string.Empty,errormessage=message});
        }


        [Authorize]
        [DisplayName("خروج")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return View(nameof(LoginMobile));
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



        public async Task<IActionResult> SignUpTrainer(string mobile)
        {
            ViewBag.Mobile = mobile;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUpTrainer(Application.Records.Profile.SignUpTrainerRecord signUpTrainerRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string[] permittedExtensions = { ".jpeg", ".jpg", ".png" };
                    var pics = typeof(Application.Records.Profile.SignUpTrainerRecord).GetProperties()
                        .Where(x => x.PropertyType == typeof(IFormFile))
                        .Select(x => (IFormFile)x.GetValue(signUpTrainerRecord))
                        .ToList();

                    var picsWithData = pics.Where(z => z != null);

                    foreach (var pic in picsWithData)
                    {
                        var ext = Path.GetExtension(pic.FileName).ToLowerInvariant();
                        if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                        {
                            ViewData["errormessage"] = "فرمت تصاویر باید از نوع jpeg یا jpg یا png باشد";
                            return View(signUpTrainerRecord.Mobile);
                        }
                    }

                    foreach (var item in picsWithData)
                    {

                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "SignUpTrainer", signUpTrainerRecord.Mobile);

                        bool exists = Directory.Exists(path);


                        if (!exists)
                            Directory.CreateDirectory(path);

                        path = path + "\\" + item.FileName;


                        if (item.Length < 2097152)
                        {
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                                stream.Close();
                            }
                        }
                        else
                        {
                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await item.CopyToAsync(stream);
                                stream.Close();

                                // Compress the image
                                using (var image = Image.Load(path))
                                {
                                    var encoder = new JpegEncoder { Quality = 50 };
                                    image.Save(path, encoder);
                                }
                            }
                        }

                    }

                    await _userService.PreRegisterTrainer(signUpTrainerRecord);

                    return View(nameof(ConfirmSignUpTrainer));
                }

                ViewBag.Mobile = signUpTrainerRecord.Mobile;

                return View();
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
                      return View("NotFound");
            }
        }


        public async Task<IActionResult> ConfirmSignUpTrainer()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> SignUpTrainer(SignUpTrainerRecordNew signUpTrainerRecordNew)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var user = await _userService.CreateTrainer(signUpTrainerRecordNew);

        //            var claims = new List<Claim>()
        //            {
        //                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //                new Claim(ClaimTypes.Name, user.UserName ??""),
        //                new Claim(ClaimTypes.GivenName, (user.FirstName + " "+user.LastName) ?? ""),
        //                new Claim(ClaimTypes.Email, user.Email),
        //                new Claim(ClaimTypes.Role, user.Roles.First().EnglishName),
        //                new Claim(type: "ProfilePic", value: user.ProfilePic??"")

        //            };
        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var principal = new ClaimsPrincipal(identity);

        //            var peraperties = new AuthenticationProperties()
        //            {
        //                IsPersistent = true
        //            };

        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, peraperties);

        //            var message = new Message(new string[] { "ramezannia.mojtaba@gmail.com" },
        //                " ثبت نام جدید مربی", signUpTrainerRecordNew.FirstName + " " + signUpTrainerRecordNew.LastName + "-" + signUpTrainerRecordNew.Email + "-" + signUpTrainerRecordNew.Mobile);

        //            await _emailSender.SendEmailAsync(message);


        //            return RedirectToAction(nameof(Index));


        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
        //        return View("NotFound");
        //    }

        //    return View();

        //}








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

        [HttpPost]
        public async Task<IActionResult> SendTrainerOtp(OTPLoginRecord otpLoginRecord)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(otpLoginRecord.Mobile))
                    ArgumentNullException.ThrowIfNull(otpLoginRecord.Mobile);

                var message= await _userService.SendTrainerOtp(otpLoginRecord);
                if (!string.IsNullOrWhiteSpace(message))
                    return RedirectToAction("TrainerPanel", "Home", new {msg = message});

                return RedirectToAction(nameof(ConfirmTrainerSmsCode), new {mobile = otpLoginRecord.Mobile});

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IActionResult> ConfirmTrainerSmsCode(string mobile, string errormessage)
        {
            ViewBag.Mobile = mobile;
            ViewData["ErrorMessage"] = errormessage;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmTrainerSmsCode(OTPRecord confirmRegisterRecord)
        {
            string message = string.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(SignUpTrainer), new {mobile = confirmRegisterRecord.Mobile});
                }

                message = Helper.GetModelSateErroMessage(ModelState);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
            }

            return RedirectToAction(nameof(ConfirmTrainerSmsCode), new { mobile = confirmRegisterRecord.Mobile,  errormessage = message });
        }




    }
}
