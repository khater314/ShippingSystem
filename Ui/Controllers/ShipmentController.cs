using BL.Contracts.Shipment;
using Domains.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    public class ShipmentController(IShipmentService shipmentService) : Controller
    {
        private readonly IShipmentService _shipmentService = shipmentService;

        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> List()
        {
            var shipments = await _shipmentService.GetShipmentsByUserIdAsync();
            return View(shipments);
        }
        [HttpGet]
        public async Task<IActionResult> List(PaginationParameters parameters)
        {
            var shipments = await _shipmentService.GetShipmentsByUserIdAsync(
                pageNumber: parameters.PageNumber, pageSize: parameters.PageSize);
            return View(shipments);
        }
    }
}
