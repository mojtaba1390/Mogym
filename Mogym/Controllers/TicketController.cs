using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mogym.Controllers
{
    [Authorize]
    [DisplayName("گفتگو")]
    public class TicketController : Controller
    {
        [DisplayName("لیست گفتگو")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
