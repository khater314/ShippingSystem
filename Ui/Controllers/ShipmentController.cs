using BL.Contracts.Shipment;
using Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    [Authorize]
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
        [HttpGet]
        public async Task<IActionResult> Show(Guid id)
        {
            var shipment = await _shipmentService.GetByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }
    }
}
