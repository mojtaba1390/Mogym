using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mogym.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        public IActionResult Index(int trainerId)
        {
            return View();
        }


        
    }
}
