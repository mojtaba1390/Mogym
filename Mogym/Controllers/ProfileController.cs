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
        public ProfileController(ITrainerProfileService trainerProfileService, IHttpContextAccessor accessor,IMapper mapper)
        {
            _trainerProfileService = trainerProfileService;
            _accessor = accessor;
            _mapper = mapper;
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


    }
}
