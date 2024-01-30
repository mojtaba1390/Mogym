using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Plan;

namespace Mogym.Controllers
{
    [Authorize]
    public class FinanceController : Controller
    {
        private readonly IFinanceService _financeService;
        public FinanceController(IFinanceService financeService)
        {
            _financeService=financeService;
        }

        public async Task<IActionResult> ApproveForPay(WaitForPayRecord model)
        {
            try
            {
               var res= await _financeService.ApproveForPay(model);

               return View(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
