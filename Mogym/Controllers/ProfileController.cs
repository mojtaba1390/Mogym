using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mogym.Application.Interfaces;
using Mogym.Domain.Entities;
using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Mogym.Application.Records.Profile;

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
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "ProfilePic", CreateTrainerProfileRecord.ProfilePic.FileName);
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        await CreateTrainerProfileRecord.ProfilePic.CopyToAsync(stream);
                        stream.Close();
                    }
                }


                await _trainerProfileService.Update(CreateTrainerProfileRecord);

            }


            return RedirectToAction("Index", "Account");
        }


    }
}
