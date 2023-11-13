using Microsoft.AspNetCore.Mvc;
using Mogym.Common;

namespace Mogym.Controllers
{
    public class DateTimeController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> ConvertDate(string persianDate)
        {
            var englishStr = Common.Helper.PersianToEnglishNumber(persianDate);

            return Ok(Common.Helper.GetMiladiDateString(englishStr));
        }


    }
}
