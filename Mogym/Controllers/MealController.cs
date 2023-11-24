using Microsoft.AspNetCore.Mvc;

namespace Mogym.Controllers
{
    public class MealController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
