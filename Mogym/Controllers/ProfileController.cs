using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Domain.Entities;

namespace Mogym.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ITrainerProfileService _trainerProfileService;
        public ProfileController(ITrainerProfileService trainerProfileService)
        {
            _trainerProfileService=trainerProfileService;
        }
        public async Task<IActionResult> ProfileInfo(string username)
        {
            var user = await _trainerProfileService.GetByUserName(username);

            return View();
        }
    }
}
