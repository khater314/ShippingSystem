using BL.Contracts.Shipment;
using BL.DTOs;
using Domains.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;


namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ShipmentController(IShipmentService shipmentService) : ControllerBase
    {
        private readonly IShipmentService _shipmentService = shipmentService;


        [HttpGet]
        public async Task<ActionResult<ApiResponse<PagedResult<TbShipmentDTO>>>> Get
            (CancellationToken ct = default)
        {
            var response = await _shipmentService.GetShipmentsByUserIdAsync(ct: ct);
            return Ok(ApiResponse<PagedResult<TbShipmentDTO>>.SuccessResponse(response));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbShipmentDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _shipmentService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbShipmentDTO>.SuccessResponse(response));
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPost("Create")]
        public async Task Post([FromBody] TbShipmentDTO dto, CancellationToken ct = default)
        {
            await _shipmentService.CreateAsync(dto, ct);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
