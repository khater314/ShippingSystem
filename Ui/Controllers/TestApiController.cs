using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class TestApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
