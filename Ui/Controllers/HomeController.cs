using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using Domains.Enums;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Ui.Models;
using Ui.Testing;


namespace Ui.Controllers
{
    public class HomeController(IShipmentService shipmentService, IUserService userService) : Controller
    {

        private readonly IShipmentService _shipmentService = shipmentService;
        private readonly IUserService _userService = userService;

        public async Task<IActionResult> Index()
        {
            await _shipmentService.CreateAsync(await new DummyData(_userService).FillShipment());
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
