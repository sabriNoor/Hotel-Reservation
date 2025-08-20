using HotelReservation.Api.Extensions;
using HotelReservation.Application.DTOs.Booking;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingCreateRequestDto dto)
        {
            var userId = User.GetUserId();
            var result = await _bookingService.CreateBookingAsync(userId, dto);
            return Ok(result);
        }

        [HttpPut("{bookingId:guid}/status")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<IActionResult> UpdateBookingStatus(Guid bookingId, [FromBody] BookingStatusUpdateRequestDto dto)
        {
            var result = await _bookingService.UpdateBookingStatusAsync(bookingId, dto);
            return Ok(result);
        }

        [HttpGet("{bookingId:guid}")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<IActionResult> GetBookingById(Guid bookingId)
        {
            var result = await _bookingService.GetBookingByIdAsync(bookingId);
            return Ok(result);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserBookings()
        {
            var userId = User.GetUserId();
            var result = await _bookingService.GetUserBookingsAsync(userId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles =Roles.Admin)]
        public async Task<IActionResult> GetBookings([FromQuery] BookingFilterDto filterDto)
        {
            var result = await _bookingService.GetBookingsAsync(filterDto);
            return Ok(result);
        }

        [HttpPost("report")]
        [Authorize(Roles =Roles.Admin)]
        public async Task<IActionResult> GetBookingReport([FromBody] BookingReportRequestDto dto)
        {
            var result = await _bookingService.GetBookingReportAsync(dto);
            return Ok(result);
        }
    }
}
