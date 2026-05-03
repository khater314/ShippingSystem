using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserSubscriptionController(IUserSubscriptionService userSubscriptionService) : ControllerBase
    {
        private readonly IUserSubscriptionService _userSubscriptionService = userSubscriptionService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbUserSubscriptionDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _userSubscriptionService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbUserSubscriptionDTO>>.SuccessResponse(response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbUserSubscriptionDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _userSubscriptionService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbUserSubscriptionDTO>.SuccessResponse(response));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbUserSubscriptionDTO dto, CancellationToken ct = default)
        {
            await _userSubscriptionService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbUserSubscriptionDTO>.SuccessResponse(dto)
            );
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _userSubscriptionService.GetByIdAsync(id, ct);
            await _userSubscriptionService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
