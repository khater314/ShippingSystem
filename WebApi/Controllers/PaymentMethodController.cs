using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentMethodController(IPaymentMethodService paymentMethodService) : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService = paymentMethodService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbPaymentMethodDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _paymentMethodService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbPaymentMethodDTO>>.SuccessResponse(response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbPaymentMethodDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _paymentMethodService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbPaymentMethodDTO>.SuccessResponse(response));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbPaymentMethodDTO dto, CancellationToken ct = default)
        {
            await _paymentMethodService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbPaymentMethodDTO>.SuccessResponse(dto)
            );
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _paymentMethodService.GetByIdAsync(id, ct);
            await _paymentMethodService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
