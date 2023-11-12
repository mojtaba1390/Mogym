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
            var profile = await _trainerProfileService.GetByUserName(username);

            return View();
        }


        [DisplayName("اطلاعات پروفایل")]
        [Authorize]
        public async Task<IActionResult> PersonInfo()
        {
            var username = _accessor.GetUserName();
            var trainerProfile = await _trainerProfileService.GetByUserName(username);
            if (trainerProfile is null)
                return View("NotFound");


            var personInfo = _mapper.Map<TrainerProfileRecord>(trainerProfile);
            return View(personInfo);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PersonInfo(TrainerProfileRecord trainerProfileRecord)
        {

            if (ModelState.IsValid)
            {


                var path = Path.Combine(_webHostEnvironment.WebRootPath, "ProfilePic", trainerProfileRecord.ProfilePic.FileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await trainerProfileRecord.ProfilePic.CopyToAsync(stream);
                    stream.Close();
                }

                _trainerProfileService.Update(trainerProfileRecord);

            }


            return RedirectToAction("Index", "Account");
        }


    }
}
