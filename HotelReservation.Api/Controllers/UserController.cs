using HotelReservation.Api.Extensions;
using HotelReservation.Application.DTOs.User;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User/profile
        [HttpGet("profile")]
        public async Task<ActionResult<UserProfileDto>> GetUserById()
        {
            var userId = User.GetUserId();
            var user = await _userService.GetUserProfileByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        // GET: api/User
        [HttpGet]
        [Authorize(Roles =Roles.Admin)]
        public async Task<ActionResult<IReadOnlyList<UserProfileDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUserAsync();
            return Ok(users);
        }

        // PUT: api/User
        [HttpPut]
        public async Task<ActionResult<UserProfileDto>> UpdateUser([FromBody] UpdateUserDto dto)
        {
            var userId = User.GetUserId();
            var updatedUser = await _userService.UpdateUserProfileAsync(userId, dto);
            return Ok(updatedUser);
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id:guid}")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
