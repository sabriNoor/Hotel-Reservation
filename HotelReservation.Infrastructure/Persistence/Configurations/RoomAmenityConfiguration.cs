using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class RoomAmenityConfiguration : IEntityTypeConfiguration<RoomAmenity>
    {

        public void Configure(EntityTypeBuilder<RoomAmenity> builder)
        {
            builder
            .HasKey(ra => new {ra.AmenityId,ra.RoomId});
            
        }
    }
}