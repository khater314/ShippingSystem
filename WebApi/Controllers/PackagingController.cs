using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PackagingController(IPackagingService packagingService) : ControllerBase
    {
        private readonly IPackagingService _packagingService = packagingService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbPackagingDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _packagingService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbPackagingDTO>>.SuccessResponse(response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbPackagingDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _packagingService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbPackagingDTO>.SuccessResponse(response));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbPackagingDTO dto, CancellationToken ct = default)
        {
            await _packagingService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbPackagingDTO>.SuccessResponse(dto)
            );
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _packagingService.GetByIdAsync(id, ct);
            await _packagingService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
