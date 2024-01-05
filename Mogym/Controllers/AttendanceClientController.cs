using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.User;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("شاگرد حضوری")]
    public class AttendanceClientController : Controller
    {
        private readonly ITrainerPlanCostService _trainerPlanCostService;

        public AttendanceClientController(ITrainerPlanCostService trainerPlanCostService)
        {
            _trainerPlanCostService = trainerPlanCostService;
        }


        [DisplayName("پذیرش شاگرد")]
        public async Task<IActionResult> Create()
        {
            var attendanceClient = await _trainerPlanCostService.GetAttendanceClientRecord();

            return View(attendanceClient);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AttendanceClientRecord attendanceClientRecord)
        {
            return View();
        }
    }
}
