using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Domain.Entities;
using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Mogym.Application.Records.Profile;
using Microsoft.IdentityModel.Tokens;
using Mogym.Application.Records.Question;
using System.Linq;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.Identity.Client;

namespace Mogym.Controllers
{
    [DisplayName("پروفایل")]
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITrainerProfileService _trainerProfileService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(ITrainerProfileService trainerProfileService, IHttpContextAccessor accessor,IMapper mapper,IWebHostEnvironment webHostEnvironment)
        {
            _trainerProfileService = trainerProfileService;
            _accessor = accessor;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(string username)
        {

            if (!username.Equals("favicon.ico"))
            {
                if (username.ToLower() == "trainers")
                    return RedirectToAction("Trainers", "Home");
                else if(username.ToLower()== "contactus")
                    return RedirectToAction("ContactUs", "Home");
                else if(username.ToLower()== "trainerpanel")
                    return RedirectToAction("TrainerPanel", "Home");
                else if(username.ToLower()== "terms")
                    return RedirectToAction("Terms", "Home");


                var profile = await _trainerProfileService.GetByUserName(username);
                if (profile is null)
                {
                    TempData["errormessage"] = "صفحه مورد نظر پیدا نشد";

                    return View("NotFound");
                }
                return View(profile);
            }


            return View("NotFound");
        }


        [DisplayName("اطلاعات پروفایل")]
        [Authorize]
        public async Task<IActionResult> PersonInfo()
        {
            var userId = _accessor.GetUser();
            var trainerProfile = await _trainerProfileService.GetByUserId(userId);
            if (trainerProfile is null)
                return View("NotFound");


            var personInfo = _mapper.Map<CreateTrainerProfileRecord>(trainerProfile);
            return View(personInfo);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonInfo(CreateTrainerProfileRecord CreateTrainerProfileRecord)
        {

            if (ModelState.IsValid)
            {

                if (CreateTrainerProfileRecord.ProfilePic is not null)
                {
                    string[] permittedExtensions = { ".jpeg", ".jpg", ".png" };
                    var ext = Path.GetExtension(CreateTrainerProfileRecord.ProfilePic.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
                    {
                        ViewData["errormessage"] = "فرمت تصویر باید از نوع jpeg یا jpg یا png باشد";
                        return RedirectToAction("Index", "Account");
                    }


                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "ProfilePic", CreateTrainerProfileRecord.ProfilePic.FileName);

                    if (CreateTrainerProfileRecord.ProfilePic.Length < 2097152)
                    {
                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await CreateTrainerProfileRecord.ProfilePic.CopyToAsync(stream);
                            stream.Close();
                        }
                    }
                    else
                    {
                        using (FileStream stream = new FileStream(path, FileMode.Create))
                        {
                            await CreateTrainerProfileRecord.ProfilePic.CopyToAsync(stream);
                            stream.Close();

                            // Compress the image
                            using (var image = SixLabors.ImageSharp.Image.Load(path))
                            {
                                // Resize the image to reduce its dimensions
                                //image.Mutate(x => x.Resize(800, 600));

                                // Compress the image with a quality setting
                                var encoder = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder { Quality = 70 };
                                image.Save(path, encoder);
                            }
                        }
                    }


                }


                _trainerProfileService.Update(CreateTrainerProfileRecord);

            }
            else
            {
                TempData["errormessage"] = Helper.GetModelSateErroMessage(ModelState);
            }

            return RedirectToAction("Index", "Account");



        }

        [Authorize]
        [DisplayName("صفحه ی من")]
        public async Task<IActionResult> MyPage()
        {
            var trainerUserName =_accessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "";
            return RedirectToAction("Index", new {username = trainerUserName});
        }



    }
}
