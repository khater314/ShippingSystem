using BL.Contracts;
using BL.DTOs;
using Domains;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Areas.Admin.Controllers
{
    public class CarrierController(ICarrierService carrierService) : BaseController<TbCarrier, TbCarrierDTO>(carrierService)
    {
        private readonly ICarrierService _carrierService = carrierService;
    }
}
