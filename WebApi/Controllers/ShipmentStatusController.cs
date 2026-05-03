
using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShipmentStatusController(IShipmentStatusService shipmentStatusService) : ControllerBase
    {
        private readonly IShipmentStatusService _shipmentStatusService = shipmentStatusService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbShipmentStatusDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _shipmentStatusService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbShipmentStatusDTO>>.SuccessResponse(response));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbShipmentStatusDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _shipmentStatusService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbShipmentStatusDTO>.SuccessResponse(response));
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbShipmentStatusDTO dto, CancellationToken ct = default)
        {
            await _shipmentStatusService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbShipmentStatusDTO>.SuccessResponse(dto)
            );
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<TbShipmentStatusDTO> patchDoc, CancellationToken ct = default)
        {
            if (patchDoc == null)
                return BadRequest(new ApiResponse<bool> 
                    { IsSuccess = false, Message = "No data provided" });

            var dto = await _shipmentStatusService.GetByIdAsync(id, ct);

            if (dto == null)
                return NotFound(new ApiResponse<bool> 
                    { IsSuccess = false, Message = "Entity not found" });

            patchDoc.ApplyTo(dto, error => ModelState.AddModelError(
                error.AffectedObject.ToString() ?? "", 
                error.ErrorMessage
            ));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<bool> 
                    { IsSuccess = false, 
                    Message = "Invalid data", 
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList() 
                    }
                );

            await _shipmentStatusService.UpdateAsync(dto, ct);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _shipmentStatusService.GetByIdAsync(id, ct);
            await _shipmentStatusService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
