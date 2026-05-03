using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class CarrierController(ICarrierService carrierService) : ControllerBase
    {
        private readonly ICarrierService _carrierService = carrierService;


        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbCarrierDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _carrierService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbCarrierDTO>>.SuccessResponse(response));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbCarrierDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _carrierService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbCarrierDTO>.SuccessResponse(response));
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbCarrierDTO dto, CancellationToken ct = default)
        {
            await _carrierService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbCarrierDTO>.SuccessResponse(dto)
            );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _carrierService.GetByIdAsync(id, ct);
            await _carrierService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
