using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("ChangeLanguage")]
        [HttpGet]
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            var cookieValue = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                cookieValue,
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
