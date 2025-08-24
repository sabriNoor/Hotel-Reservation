using HotelReservation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelReservation.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
            .HasKey(u => u.Id);

            builder
            .OwnsOne(u => u.PersonalInformation, pi =>
                {
                    pi.Property(p => p.FirstName);
                    pi.Property(p => p.LastName);
                    pi.Property(p => p.MobileNumber);
                });

            builder.
            HasIndex(u => u.Username)
            .IsUnique();

            builder
            .HasIndex(u => u.Email)
            .IsUnique();

            builder
            .HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);

        }
    }
}