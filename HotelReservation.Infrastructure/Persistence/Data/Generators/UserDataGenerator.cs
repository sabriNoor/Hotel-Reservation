using Bogus;
using HotelReservation.Domain.Entities;
using HotelReservation.Domain.Enums;
using HotelReservation.Domain.ValueObjects;

namespace HotelReservation.Infrastructure.Persistence.Data.Generators
{
    public class UserDataGenerator
    {
        private const UserRole _userRole = UserRole.User;
        private const UserRole _adminRole = UserRole.Admin;
        public static List<User> GenerateUsers(int count = 10)
        {
            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Username, f => f.Internet.UserName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.PasswordHash, f => "$2a$11$b.0eGeZIhk2Txnpl6YrYl.8TS7S8kcXt9x9AFktVgWRe7cmYt.92C") 
                .RuleFor(u => u.Role, f => _userRole)
                .RuleFor(u => u.PersonalInformation, f => new PersonalInformation
                {
                    FirstName = f.Name.FirstName(),
                    LastName = f.Name.LastName(),
                    MobileNumber = f.Phone.PhoneNumber("##########")
                });

            var users = userFaker.Generate(count);
            users[0].Role = _adminRole;
            users[0].Username = "admin";
            users[0].Email = "admin@gmail.com";

            return users;
        }

    }
}