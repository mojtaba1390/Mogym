using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces.ICache;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using StackExchange.Redis;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Mogym.Common;
using Microsoft.AspNetCore.Authorization;

namespace Mogym.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserService userService, IMenuService menuService, IRedisCacheService redisCacheService, IConfiguration configuration)
        {
            _userService = userService;
            _menuService = menuService;
            _redisCacheService = redisCacheService;
            _configuration = configuration;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRecord loginRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var confirmSmsCode = await _userService.LoginAsync(loginRecord);
                    ArgumentNullException.ThrowIfNull(confirmSmsCode);
                    return Ok(loginRecord.Mobile);
                }

                return BadRequest(Helper.GetModelSateErroMessage(ModelState));
            }
            catch (Exception e)
            {
                return Problem();
            }


        }


        /// <summary>
        /// اگه کد ارسالی و موبایل درست باشه، اطلاعات کاربر و نقش و دسترسی و منو های با وضعیت فعال گرفته میشه و در ردیس ذخیره میشه
        /// و بعد از اون، یه سری از اطلاعات برای کوکی ذخیره میشه
        /// </summary>
        /// <param name="confirmRegisterRecord"></param>
        /// <returns></returns>
        [HttpPost("ConfirmCode")]
        public async Task<IActionResult> ConfirmSmsCode([FromBody] ConfirmSmsRecord confirmRegisterRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = await _userService.GetUserWithRoleAndPermission(confirmRegisterRecord.Mobile);
                    var activeMenus = await _menuService.GetAllActiveMenuList();

                    var userInfoKey = _configuration.GetSection("RedisKey").GetValue<string>("UserInformation");
                    var activeMenusKey = _configuration.GetSection("RedisKey").GetValue<string>("MenuList");



                    //TODO: اینجا باید اول کلید های مربوطه تو ردیس پاک بشه بعد ست بشه

                    #region set redis cache
                    var isRedisConnected = (await ConnectionMultiplexer.ConnectAsync(_configuration.GetConnectionString("RedisConnection") ?? string.Empty)).IsConnected;
                    if (isRedisConnected)
                    {
                        await _redisCacheService.Set(userInfoKey, user, DateTime.Now.AddDays(1).Minute,
                            DateTime.Now.AddHours(1).Minute);
                        await _redisCacheService.Set(activeMenusKey, activeMenus, DateTime.Now.AddDays(1).Minute,
                            DateTime.Now.AddHours(1).Minute);
                    }

                    #endregion


                    var authClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName ??""),
                        new Claim(ClaimTypes.GivenName, (user.FirstName + " "+user.LastName) ?? ""),
                        new Claim(ClaimTypes.MobilePhone, user.Mobile)

                    };
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Secret));

                    var token = new JwtSecurityToken(
                        issuer: JWTSettings.Issuer,
                        audience: JWTSettings.Audiance,
                        expires: DateTime.Now.AddHours(JWTSettings.ExpireTime),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    var identity = new ClaimsIdentity(authClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var peraperties = new AuthenticationProperties()
                    {
                        IsPersistent = true

                    };

                    return Ok();//TODO:چی پاس بدم؟!
                }

                return BadRequest(Helper.GetModelSateErroMessage(ModelState));
            }
            catch (Exception e)
            {
                return Problem();
            }

        }


        [Authorize]
        [HttpGet("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }

        [HttpPost("SignUpTrainer")]
        public async Task<IActionResult> SignUpTrainer([FromBody] SignUpTrainerRecord signUpTrainerRecord )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var confirmSmsCode = await _userService.SignUpTrainer(signUpTrainerRecord);
                    ArgumentNullException.ThrowIfNull(confirmSmsCode);
                    return Ok(signUpTrainerRecord.Mobile);
                }

                return BadRequest(Helper.GetModelSateErroMessage(ModelState));
            }
            catch (Exception e)
            {
                return Problem();
            }

        }

    }
}
