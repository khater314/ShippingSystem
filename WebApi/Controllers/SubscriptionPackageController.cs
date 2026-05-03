using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubscriptionPackageController(ISubscriptionPackageService subscriptionPackageService) : ControllerBase
    {
        private readonly ISubscriptionPackageService _subscriptionPackageService = subscriptionPackageService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbSubscriptionPackageDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _subscriptionPackageService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbSubscriptionPackageDTO>>.SuccessResponse(response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbSubscriptionPackageDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _subscriptionPackageService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbSubscriptionPackageDTO>.SuccessResponse(response));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbSubscriptionPackageDTO dto, CancellationToken ct = default)
        {
            await _subscriptionPackageService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbSubscriptionPackageDTO>.SuccessResponse(dto)
            );
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _subscriptionPackageService.GetByIdAsync(id, ct);
            await _subscriptionPackageService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
