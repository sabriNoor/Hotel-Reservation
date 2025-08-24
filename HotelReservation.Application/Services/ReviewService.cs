using AutoMapper;
using HotelReservation.Application.DTOs.Review;
using HotelReservation.Application.Exceptions;
using HotelReservation.Application.IRepository;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Entities;
using Microsoft.Extensions.Logging;
using UnauthorizedAccessException = HotelReservation.Application.Exceptions.UnauthorizedAccessException;

namespace HotelReservation.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ILogger<ReviewService> _logger;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ReviewService(
            ILogger<ReviewService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _reviewRepository = _unitOfWork.ReviewRepository;

        }
        public async Task<ReviewResponseDto> CreateReviewAsync(Guid userId, Guid bookingId, ReviewCURequestDto dto)
        {
            try
            {
                var booking = await _unitOfWork.BookingRepository
                    .FindOneAsync(b => b.Id == bookingId)
                    ?? throw new NotFoundException("Booking", bookingId);

                if (booking.UserId != userId)
                    throw new UnauthorizedAccessException();

                var review = _mapper.Map<Review>(dto);
                review.BookingId = bookingId;

                await _reviewRepository.AddAsync(review);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation(
                    "Review {ReviewId} created successfully for Booking {BookingId} by User {UserId}.",
                    review.Id, bookingId, userId
                );

                return _mapper.Map<ReviewResponseDto>(review);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Booking not found: {BookingId}", bookingId);
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access attempt by User {UserId} on Booking {BookingId}.", userId, bookingId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating review for Booking {BookingId} by User {UserId}.", bookingId, userId);
                throw;
            }
        }


        public async Task DeleteReviewAsync(Guid userId, Guid reviewId)
        {
            try
            {
                var review = await _reviewRepository
                    .GetReviewDetailAsync(reviewId)
                    ?? throw new NotFoundException("Review", reviewId);

                if (review.Booking.User.Id != userId)
                    throw new UnauthorizedAccessException();

                await _reviewRepository.DeleteAsync(reviewId);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Review {ReviewId} deleted successfully.", reviewId);


            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Review not found: {ReviewId}", reviewId);
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access attempt by User {UserId} on Review {ReviewId}.", userId, reviewId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting review {ReviewId}.", reviewId);
                throw;
            }
        }

        public async Task<RoomReviewsResponseDto> GetAllReviewsAsync(Guid roomId)
        {
            try
            {
                var reviews = await _reviewRepository.GetRoomReviewsAsync(roomId);

                var reviewsCount = reviews.Count;

                var averageRating = reviewsCount > 0
                    ? reviews.Sum(r => r.Rating) / (double)reviewsCount
                    : 0;                

                var response = new RoomReviewsResponseDto
                {
                    ReviewsCount = reviewsCount,
                    AverageRating = averageRating,
                    Reviews = [.. reviews.Select(r=>_mapper.Map<ReviewDetailDto>(r))]
                };

                _logger.LogInformation("Fetched {ReviewsCount} reviews for Room {RoomId}.", reviewsCount, roomId);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch reviews for Room {RoomId}.", roomId);
                throw;
            }
        }


        public async Task<ReviewResponseDto> UpdateReviewAsync(Guid userId, Guid reviewId, ReviewCURequestDto dto)
        {
            try
            {
                var review = await _reviewRepository
                    .GetReviewDetailAsync(reviewId)
                    ?? throw new NotFoundException("Review", reviewId);

                if (review.Booking.User.Id != userId)
                    throw new UnauthorizedAccessException();

                _mapper.Map(dto, review);
                _reviewRepository.Update(review);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation(
                   "Review {ReviewId} updated successfully for Booking {BookingId} by User {UserId}.",
                   review.Id, review.BookingId, userId
               );

                return _mapper.Map<ReviewResponseDto>(review);

            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Review not found: {ReviewId}", reviewId);
                throw;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Unauthorized access attempt by User {UserId} on Review {ReviewId}.", userId, reviewId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating review {ReviewId}.", reviewId);
                throw;
            }
        }
    }
}