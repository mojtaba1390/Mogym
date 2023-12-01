using Microsoft.AspNetCore.Mvc;

namespace Mogym.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
