using HotelReservation.Application.DTOs.Amenity;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles =Roles.Admin)]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<AmenityResponseDto>> GetById(Guid id)
        {
            var amenity = await _amenityService.GetAmenityByIdAsync(id);
            return Ok(amenity);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AmenityResponseDto>>> GetAll()
        {
            var amenities = await _amenityService.GetAllAmenityAsync();
            return Ok(amenities);
        }

        [HttpPost]
        public async Task<ActionResult<AmenityResponseDto>> Add([FromBody] AmenityCURequestDto dto)
        {
            var amenity = await _amenityService.AddAmenityAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = amenity.Id }, amenity);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<AmenityResponseDto>> Update(Guid id, [FromBody] AmenityCURequestDto dto)
        {
            var updated = await _amenityService.UpdateAmenityAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _amenityService.DeleteAmenityAsync(id);
            return NoContent();
        }
    }
}
