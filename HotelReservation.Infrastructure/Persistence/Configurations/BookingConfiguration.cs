using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder
            .HasKey(b => b.Id);

            builder
            .OwnsOne(b => b.TotalPrice, p =>
                {
                    p.Property(p => p.Amount);
                    p.Property(p => p.Currency);
                });
            
             builder
            .OwnsOne(b => b.Stay, p =>
                {
                    p.Property(p => p.CheckIn);
                    p.Property(p => p.CheckOut);
                });
        }
    }
}