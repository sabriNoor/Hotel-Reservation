using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelReservation.Domain.Enums;

namespace HotelReservation.Application.DTOs.Booking
{
    public record class BookingFilterDto
    (
        DateTime? StartDate = null,
        DateTime? EndDate = null,
        BookingStatus? Status = null

    );
}