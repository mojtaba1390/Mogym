using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Mogym.Application.Records.Plan;
using System.ComponentModel;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("مالی")]
    public class FinanceController : Controller
    {
        private readonly IFinanceService _financeService;
        private readonly IHttpContextAccessor _accessor;
        public FinanceController(IFinanceService financeService,IHttpContextAccessor accessor)
        {
            _financeService=financeService;
            _accessor=accessor;
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
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
                return View("NotFound");
            }
        }

        [DisplayName("تاریخچه")]
        public async Task<IActionResult> History()
        {
            try
            {
                var res = await _financeService.GetFinanceHistory();

                return View(res);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = "خطایی در سیستم رخ داده است,لطفا دوباره سعی کنید";
                return View("NotFound");
            }
        }
    }
}
