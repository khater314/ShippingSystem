using BL.Contracts;
using BL.DTOs;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityController(ICityService cityService) : ControllerBase
    {
        private readonly ICityService _cityService = cityService;

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbCityDTO>>>> Get
            (CancellationToken ct = default)
        {
            var response = await _cityService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbCityDTO>>.SuccessResponse(response));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbCityDTO>>> Get(Guid id, 
            CancellationToken ct = default)
        {
            var response = await _cityService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbCityDTO>.SuccessResponse(response));
        }


        [HttpGet("country-id/{countryId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbCityDTO>>>> GetByCountry
            (Guid countryId, CancellationToken ct = default)
        {
            var response = await _cityService.GetCitiesByCountryIdAsync(countryId, ct);
            return Ok(ApiResponse<IEnumerable<TbCityDTO>>.SuccessResponse(response));
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbCityDTO dto, CancellationToken ct = default)
        {
            await _cityService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbCityDTO>.SuccessResponse(dto)
            );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _cityService.GetByIdAsync(id, ct);
            await _cityService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
