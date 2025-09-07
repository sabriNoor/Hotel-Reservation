using System;
using System.Collections.Generic;
using Bogus;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Infrastructure.Persistence.Data.Generators
{
    public class RoomDataGenerator
    {
        public static List<Room> GenerateRooms(List<Amenity> amenities, int count = 10)
        {
            var roomNumber = 100;

            var faker = new Faker<Room>()
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(r => r.Number, f => roomNumber++)
                .RuleFor(r => r.Type, f => f.PickRandom<RoomType>())
                .RuleFor(r => r.Description, (f, r) => $"{r.Type} room with comfortable amenities.")
                .RuleFor(r => r.Capacity, (f, r) => r.Type switch
                {
                    RoomType.Single => 1,
                    RoomType.Double => 2,
                    RoomType.Suite => 4,
                    _ => 1
                })
                .RuleFor(r => r.PricePerNight, f => new Money() { Amount = f.Random.Decimal(50, 500) })
                .RuleFor(r => r.RoomAmenities, f =>
                    f.Random.ListItems(amenities, f.Random.Int(1, amenities.Count))
                        .Select(a => new RoomAmenity { Amenity = a })
                        .ToList()
                );
              

            return faker.Generate(count);
        }
    }
}
