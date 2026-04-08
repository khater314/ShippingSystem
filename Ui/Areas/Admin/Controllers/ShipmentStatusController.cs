using BL.Contracts;
using BL.DTOs;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    public class ShipmentStatusController(IShipmentStatusService shipmentStatusService) : BaseController<TbShipmentStatus, TbShipmentStatusDTO>(shipmentStatusService)
    {
        private readonly IShipmentStatusService _shipmentStatusService = shipmentStatusService;
    }
}
