using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder
            .HasKey(a => a.Id);

            builder
            .HasMany(a => a.RoomAmenities)
            .WithOne(ra => ra.Amenity)
            .HasForeignKey(ra => ra.AmenityId);
        }
    }
}