using System;
using System.Collections.Generic;
using Bogus;
using HotelReservation.Domain.Entities;

namespace HotelReservation.Infrastructure.Persistence.Data.Generators
{
    public class AmenityDataGenerator
    {
        private static readonly List<string> AmenityNames = [
            "Air Conditioning","Balcony","Mini Fridge","Flat Screen TV","Safe Box",
            "Coffee Maker","Hair Dryer","Desk & Chair","WiFi","Room Service"
        ];
        
        public static List<Amenity> GenerateAmenities(int count = 5)
        {
            var selectedNames = AmenityNames.OrderBy(_ => Guid.NewGuid()).Take(count).ToList();

            var faker = new Faker<Amenity>()
                .RuleFor(a => a.Id, f => Guid.NewGuid())
                .RuleFor(a => a.Name, f => selectedNames[f.IndexFaker % selectedNames.Count])
                .RuleFor(a => a.Description, (f, a) => $"{a.Name} available for guests.");

            return faker.Generate(count);
        }
    }
}
