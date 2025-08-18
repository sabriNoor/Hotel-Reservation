using HotelReservation.Domain.Entities;
using HotelReservation.Infrastructure.Persistence.Data.Generators;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Infrastructure.Persistence.Data
{
    public class DataSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var usersCount = await context.Users.CountAsync();
            var amenitiesCount = await context.Amenities.CountAsync();
            var roomsCount = await context.Rooms.CountAsync();
            List<User> users = [];
            List<Amenity> amenities = [];
            List<Room> rooms = [];
            if (amenitiesCount == 0)
            {
                users = UserDataGenerator.GenerateUsers(4);
            }
            if (amenitiesCount == 0)
            {
                amenities = AmenityDataGenerator.GenerateAmenities(7);
            }
            if (roomsCount == 0)
            {
                rooms = RoomDataGenerator.GenerateRooms(amenities,5);
            }

            await context.Users.AddRangeAsync(users);
            await context.Amenities.AddRangeAsync(amenities);
            await context.Rooms.AddRangeAsync(rooms);
            await context.SaveChangesAsync();


        }
    }
}