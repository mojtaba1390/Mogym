using Microsoft.AspNetCore.Mvc;
using Mogym.Application.Interfaces;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Mogym.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService=discountService;
        }


        public async Task<JsonResult> GetDiscountPrice(string discountText,int trainingPlanId)
        {
            try
            {
                var res = await _discountService.GetDiscountPrice(discountText, trainingPlanId);

                return new JsonResult(res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
