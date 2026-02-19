using BL.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ui.Models;
using Microsoft.AspNetCore.Localization;

namespace Ui.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> _logger;
        readonly ICityService _tableRepository;
        public HomeController(ILogger<HomeController> logger, ICityService tableRepository)
        {
            _logger = logger;
            _tableRepository = tableRepository;
        }
        public async Task<IActionResult> Index()
        {
            var entities = await _tableRepository.GetAllAsync();
            return View();
        }

        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
