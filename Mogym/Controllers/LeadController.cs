using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Lead;

namespace Mogym.Controllers
{
    public class LeadController : Controller
    {
        private readonly ILeadService _leadService;
        public LeadController(ILeadService leadService)
        {
            _leadService=leadService;
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Submit(LeadRecord leadRecord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _leadService.Submit(leadRecord);
                    return View("ThankYou");
                }

                return RedirectToAction("ContactUs", "Home");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
