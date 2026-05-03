using BL.Contracts;
using BL.DTOs;
using BL.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserContactController(IUserContactService userContactService) : ControllerBase
    {
        private readonly IUserContactService _userContactService = userContactService;


        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TbUserContactDTO>>>> Get(CancellationToken ct = default)
        {
            var response = await _userContactService.GetAllAsync(ct);
            return Ok(ApiResponse<IEnumerable<TbUserContactDTO>>.SuccessResponse(response));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TbUserContactDTO>>> Get(Guid id, CancellationToken ct = default)
        {
            var response = await _userContactService.GetByIdAsync(id, ct);
            return Ok(ApiResponse<TbUserContactDTO>.SuccessResponse(response));
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TbUserContactDTO dto, CancellationToken ct = default)
        {
            await _userContactService.AddAsync(dto, ct);
            return CreatedAtAction(
                nameof(Get),
                new { id = dto.Id },
                ApiResponse<TbUserContactDTO>.SuccessResponse(dto)
            );
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<TbUserContactDTO> patchDoc, CancellationToken ct = default)
        {
            if (patchDoc == null)
                return BadRequest(new ApiResponse<bool>
                { IsSuccess = false, Message = "No data provided" });

            var dto = await _userContactService.GetByIdAsync(id, ct);

            if (dto == null)
                return NotFound(new ApiResponse<bool>
                { IsSuccess = false, Message = "Entity not found" });

            patchDoc.ApplyTo(dto, error => ModelState.AddModelError(
                error.AffectedObject.ToString() ?? "",
                error.ErrorMessage
            ));

            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<bool>
                {
                    IsSuccess = false,
                    Message = "Invalid data",
                    Errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                });

            await _userContactService.UpdateAsync(dto, ct);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct = default)
        {
            var dto = await _userContactService.GetByIdAsync(id, ct);
            await _userContactService.ChangeStatusAsync(dto, 2, ct);
            return NoContent();
        }
    }
}
