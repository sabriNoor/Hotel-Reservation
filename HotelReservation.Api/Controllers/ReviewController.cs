using HotelReservation.Api.Extensions;
using HotelReservation.Application.DTOs.Review;
using HotelReservation.Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // POST: api/Review/{bookingId}
        [HttpPost("{bookingId:guid}")]
        public async Task<ActionResult<ReviewResponseDto>> CreateReview(
            Guid bookingId, [FromBody] ReviewCURequestDto dto)
        {
            var userId = User.GetUserId();
            await _reviewService.CreateReviewAsync(userId, bookingId, dto);
            return Created();
        }

        // PUT: api/Review/{reviewId}
        [HttpPut("{reviewId:guid}")]
        public async Task<ActionResult<ReviewResponseDto>> UpdateReview(
           Guid reviewId, [FromBody] ReviewCURequestDto dto)
        {
            var userId = User.GetUserId();
            var updatedReview = await _reviewService.UpdateReviewAsync(userId, reviewId, dto);
            return Ok(updatedReview);
        }

        // GET: api/Review/{roomId}
        [HttpGet("{roomId:guid}")]
        public async Task<ActionResult<RoomReviewsResponseDto>> GetAllReviews(Guid roomId)
        {
            var reviews = await _reviewService.GetAllReviewsAsync(roomId);
            return Ok(reviews);
        }

        // DELETE: api/Review/{reviewId}
        [HttpDelete("{reviewId:guid}")]
        public async Task<IActionResult> DeleteReview(Guid reviewId)
        {
            var userId = User.GetUserId();
            await _reviewService.DeleteReviewAsync(userId, reviewId);
            return NoContent();
        }
    }
}
