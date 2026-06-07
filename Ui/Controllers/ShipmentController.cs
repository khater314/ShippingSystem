using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class ShipmentController : Controller
    {
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            return View();
        }
    }
}
