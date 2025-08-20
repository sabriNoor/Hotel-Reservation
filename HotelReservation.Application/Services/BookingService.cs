using AutoMapper;
using HotelReservation.Application.DTOs.Booking;
using HotelReservation.Application.Exceptions;
using HotelReservation.Application.IRepository;
using HotelReservation.Application.IServices;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace HotelReservation.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly ILogger<BookingService> _logger;
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        public BookingService(
            ILogger<BookingService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _bookingRepository = _unitOfWork.BookingRepository;

        }
        public async Task<BookingResponseDto> CreateBookingAsync(Guid userId, BookingCreateRequestDto dto)
        {
            try
            {
                var booking = _mapper.Map<Booking>(dto);
                booking.UserId = userId;

                var room = await _unitOfWork.RoomRepository.GetRoomWithBookingsAsync(booking.RoomId)
                    ?? throw new NotFoundException("Room", booking.RoomId);

                if (booking.NumberOfGuests > room.Capacity)
                    throw new BusinessException($"Room capacity ({room.Capacity}) is less than number of guests ({booking.NumberOfGuests}).");

                var canBook = await CanBookAsync(room, booking.Stay);
                if (!canBook)
                    throw new BusinessException("Room is not available for the selected date range.");

                await _bookingRepository.AddAsync(booking);
                await _unitOfWork.CompleteAsync();

                _logger.LogInformation("Booking created successfully for Room {RoomId} by User {UserId}", room.Id, userId);

                return _mapper.Map<BookingResponseDto>(booking);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Room {RoomId} not found", dto.RoomId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking for User {UserId}", userId);
                throw;
            }
        }

        private async Task<bool> CanBookAsync(Room room, DateRange stay)
        {
            if (!room.IsAvailable) return false;

           return !await _bookingRepository.ExistsAsync(b =>
                b.RoomId == room.Id &&
                b.Status != BookingStatus.Cancelled &&
                b.Stay.CheckIn < stay.CheckOut &&
                stay.CheckIn < b.Stay.CheckOut
            );
        }

        public async Task<BookingResponseDto> UpdateBookingStatusAsync(Guid bookingId, BookingStatusUpdateRequestDto dto)
        {
            try
            {
                var booking = await _bookingRepository.GetByIdAsync(bookingId)
                    ?? throw new NotFoundException("Booking", bookingId);

                if (booking.Status == BookingStatus.Pending)
                {
                    booking.Status = dto.Status;
                    await _unitOfWork.CompleteAsync();

                    _logger.LogInformation("Booking {BookingId} status updated successfully to {Status}", booking.Id, booking.Status);
                }
                else
                {
                    _logger.LogWarning("Attempt to update booking {BookingId} denied. Current status: {CurrentStatus}", booking.Id, booking.Status);
                    throw new InvalidOperationException($"Cannot update booking {booking.Id} because its status is {booking.Status}");
                }

                return _mapper.Map<BookingResponseDto>(booking);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Booking {BookingId} not found", bookingId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating booking {BookingId}", bookingId);
                throw;
            }
        }

        public async Task<BookingDetailResponseDto> GetBookingByIdAsync(Guid bookingId)
        {
            try
            {
                var booking = await _bookingRepository.GetBookingDetailAsync(bookingId)
                    ?? throw new NotFoundException("Booking", bookingId);
                _logger.LogInformation("Booking with ID {BookingId} retrieved successfully.", bookingId);
                return _mapper.Map<BookingDetailResponseDto>(booking);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Booking {BookingId} not found", bookingId);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving booking {BookingId}", bookingId);
                throw;
            }
        }

        public async Task<IReadOnlyList<BookingResponseDto>> GetUserBookingsAsync(Guid userId)
        {
            try
            {
                var bookings = await _bookingRepository.FindAsync(b => b.User.Id == userId);
                _logger.LogInformation(
                    "Successfully retrieved {BookingCount} bookings from the database for user with ID {userID}."
                    , bookings.Count(), userId
                );
                return _mapper.Map<IReadOnlyList<BookingResponseDto>>(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during getting bookings of user with ID {UserId}", userId);
                throw;
            }
        }

        public async Task<IReadOnlyList<BookingDetailResponseDto>> GetBookingsAsync(BookingFilterDto filterDto)
        {
            try
            {
                var query = _bookingRepository.GetBookingsWithDetails(
                    filterDto.StartDate,
                    filterDto.EndDate,
                    filterDto.Status
                );

                var bookings = await query
                        .AsNoTracking()
                        .OrderBy(b => b.Stay.CheckIn)
                        .ToListAsync();

                _logger.LogInformation(
                    "Retrieved {Count} bookings with filters: startDate={StartDate}, endDate={EndDate}, status={Status}",
                    bookings.Count, filterDto.StartDate, filterDto.EndDate, filterDto.Status
                );

                return _mapper.Map<List<BookingDetailResponseDto>>(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error occurred while retrieving bookings with filters: startDate={StartDate}, endDate={EndDate}, status={Status}",
                    filterDto.StartDate, filterDto.EndDate, filterDto.Status
                    );
                throw;
            }
        }

        public async Task<BookingReportResponseDto> GetBookingReportAsync(BookingReportRequestDto dto)
        {
            try
            {
                var query = _bookingRepository.GetBookingsWithDetails(dto.StartDate, dto.EndDate);
                var bookings = await query.ToListAsync();
                var totalBookings = bookings.Count();
                var totalRevenue = bookings.Sum(b => b.TotalPrice.Amount);
                var bookingsByStatus = bookings.GroupBy(b => b.Status).ToDictionary(g => g.Key.ToString(), g => g.Count());
                var report = new BookingReportResponseDto()
                {
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                    TotalBookings = totalBookings,
                    TotalRevenue = totalRevenue,
                    BookingsByStatus = bookingsByStatus

                };
                _logger.LogInformation(
                    "Generated booking report from {StartDate} to {EndDate} with {TotalBookings} bookings",
                    dto.StartDate, dto.EndDate, totalBookings
                );

                return report;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error generating booking report from {StartDate} to {EndDate}",
                    dto.StartDate, dto.EndDate
                );
                throw;
            }
        }
    }
}