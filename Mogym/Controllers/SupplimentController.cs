using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Ingridient;
using Mogym.Application.Records.Suppliment;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("مکمل")]
    public class SupplimentController : Controller
    {
        private readonly ISupplimentService _supplimentService;
        public SupplimentController(ISupplimentService supplimentService)
        {
            _supplimentService = supplimentService;
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
    }
}
