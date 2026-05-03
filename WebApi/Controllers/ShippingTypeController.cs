using BL.Contracts;
using BL.DTOs;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShippingTypeController(IShippingTypeService shippingTypeService) : ControllerBase
    {
        private readonly IShippingTypeService _shippingTypeService = shippingTypeService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbShippingTypeDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _shippingTypeService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbShippingTypeDTO>>.SuccessResponse(response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbShippingTypeDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _shippingTypeService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbShippingTypeDTO>.SuccessResponse(response));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbShippingTypeDTO dto, CancellationToken ct = default)
        {
            await _shippingTypeService.AddAsync(dto, ct);
            return  CreatedAtAction(
                nameof(Get), 
                new { id = dto.Id }, 
                ApiResponse<TbShippingTypeDTO>.SuccessResponse(dto)
            );
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _shippingTypeService.GetByIdAsync(id, ct); 
            await _shippingTypeService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
