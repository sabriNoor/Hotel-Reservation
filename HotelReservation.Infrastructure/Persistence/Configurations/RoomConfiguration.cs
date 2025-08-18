using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder
            .HasKey(r => r.Id);

            builder
            .OwnsOne(r => r.PricePerNight, p =>
                {
                    p.Property(p => p.Amount);
                    p.Property(p => p.Currency);
                });

            builder
            .HasIndex(r => r.Number)
            .IsUnique();

            builder
            .HasMany(r => r.RoomAmenities)
            .WithOne(ra => ra.Room)
            .HasForeignKey(ra => ra.RoomId);

            builder
            .HasMany(r => r.Bookings)
            .WithOne(b => b.Room)
            .HasForeignKey(b=>b.RoomId);
            

        }
    }
}