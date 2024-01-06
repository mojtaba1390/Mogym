using System.ComponentModel;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;
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
        private readonly IPlanService _planService;

        public AttendanceClientController(ITrainerPlanCostService trainerPlanCostService, IPlanService planService)
        {
            _trainerPlanCostService = trainerPlanCostService;
            _planService = planService;
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


            try
            {
                await _planService.AddAttendancClientRequest(attendanceClientRecord);
                return RedirectToAction("AttendanceClientRequest", "Plan");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
                return View("NotFound");
            }


            return View();
        }




    }
}
