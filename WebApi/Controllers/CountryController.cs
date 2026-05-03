using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryController(ICountryService countryService) : ControllerBase
    {
        private readonly ICountryService _countryService = countryService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbCountryDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _countryService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbCountryDTO>>.SuccessResponse(response));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbCountryDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _countryService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbCountryDTO>.SuccessResponse(response));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbCountryDTO dto, CancellationToken ct = default)
        {
            await _countryService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbCountryDTO>.SuccessResponse(dto)
            );
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _countryService.GetByIdAsync(id, ct);
            await _countryService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
