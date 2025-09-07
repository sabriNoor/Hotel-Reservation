using HotelReservation.Application.DTOs.Room;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET: api/Room/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RoomResponseDto>> GetRoomById(Guid id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
                return NotFound(new { message = "Room not found" });

            return Ok(room);
        }

        // GET: api/Room
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<RoomResponseDto>>> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomAsync();
            return Ok(rooms);
        }

        // POST: api/Room
        [HttpPost]
        [Authorize(Roles =Roles.Admin)]
        public async Task<ActionResult<RoomResponseDto>> AddRoom([FromBody] RoomCURequestDto dto)
        {
            var room = await _roomService.AddRoomAsync(dto);
            return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
        }

        // PUT: api/Room/{id}
        [HttpPut("{id:guid}")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<ActionResult<RoomResponseDto>> UpdateRoom(Guid id, [FromBody] RoomCURequestDto dto)
        {
            var updatedRoom = await _roomService.UpdateRoomAsync(id, dto);
            return Ok(updatedRoom);
        }

        // DELETE: api/Room/{id}
        [HttpDelete("{id:guid}")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            await _roomService.DeleteRoomAsync(id);
            return NoContent();
        }

        // POST: api/Room/available
        [HttpPost("available")]
        public async Task<ActionResult<IReadOnlyList<RoomResponseDto>>> GetAvailableRooms([FromBody] RoomFilterDto filter)
        {
            var availableRooms = await _roomService.GetAvailableRoomsAsync(filter);
            return Ok(availableRooms);
        }
    }
}
