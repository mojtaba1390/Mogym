using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Ingridient;
using Mogym.Application.Records.Suppliment;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("مکمل")]
    public class SupplimentController : Controller
    {
        private readonly ISupplimentService _supplimentService;
        private readonly IPlanService _planService;
        private readonly ISupplimentPlanDetailService _supplimentPlanDetailService;
        public SupplimentController(ISupplimentService supplimentService, IPlanService planService, ISupplimentPlanDetailService supplimentPlanDetailService)
        {
            _supplimentService = supplimentService;
            _planService = planService;
            _supplimentPlanDetailService = supplimentPlanDetailService;
        }
        [DisplayName("لیست مکمل ها")]
        public async Task<IActionResult> Index()
        {
            var suppliments = await _supplimentService.GetAll();

            return View(suppliments);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplimentRecord ingredientRecord)
        {
            if (ModelState.IsValid)
            {

                await _supplimentService.AddAsync(ingredientRecord);

            }


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, string title)
        {
            try
            {
                await _supplimentService.Edit(id, title);
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = "خطایی در سیستم رخ داده است";
            }
            return View("NotFound");
        }


    }
}
