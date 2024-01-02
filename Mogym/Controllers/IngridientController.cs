using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.ExerciseVideo;
using Mogym.Application.Records.Ingridient;
using Mogym.Application.Services;

namespace Mogym.Controllers
{
    [DisplayName("مواد غذایی")]
    [Authorize]
    public class IngridientController : Controller
    {
        private readonly IIngridientService _ingridientService;
        public IngridientController(IIngridientService ingridientService)
        {
            _ingridientService=ingridientService;
        }

        [DisplayName("لیست مواد غذایی")]
        public async Task<IActionResult> Index()
        {
            var ingridients = await _ingridientService.GetAll();

            return View(ingridients);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IngredientRecord ingredientRecord)
        {
            if (ModelState.IsValid)
            {

                await _ingridientService.AddAsync(ingredientRecord);

            }


            return RedirectToAction(nameof(Create));
        }
    }
}
