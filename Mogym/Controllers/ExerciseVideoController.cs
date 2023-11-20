using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Mogym.Controllers
{
    [DisplayName("ویدئو تمرینات")]
    public class ExerciseVideoController : Controller
    {

        [DisplayName("لیست ویدئو ها")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
