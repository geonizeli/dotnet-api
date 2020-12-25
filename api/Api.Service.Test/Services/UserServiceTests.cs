using System;
using System.Collections.Generic;
using Api.Domain.Dtos;
using Api.Domain.Dtos.User;

namespace Api.Service.Test.Services
{
    public class UserServiceTests
    {
        public static string Name { get; set; }
        public static string Email { get; set; }
        public static string ChangedName { get; set; }
        public static string ChangedEmail { get; set; }
        public static Guid Id { get; set; }
        public List<UserDto> listUserDto = new List<UserDto>();
        public UserDto userDto;
        public UserCreateDto userCreateDto;
        public UserCreateResultDto userCreateResultDto;
        public UserUpdateDto userUpdateDto;
        public UserUpdateResultDto userUpdateResultDto;
        public UserServiceTests()
        {
            Id = Guid.NewGuid();
            Name = Faker.Name.FullName();
            Email = Faker.Internet.Email();
            ChangedName = Faker.Name.FullName();
            ChangedEmail = Faker.Internet.Email();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                };
                listUserDto.Add(dto);
            }

            userDto = new UserDto()
            {
                Id = Id,
                Name = Name,
                Email = Email
            };

            userCreateDto = new UserCreateDto()
            {
                Name = Name,
                Email = Email
            };

            userCreateResultDto = new UserCreateResultDto()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                CreatedAt = DateTime.UtcNow
            };

            userUpdateDto = new UserUpdateDto()
            {
                Id = Id,
                Name = Name,
                Email = Email
            };

            userUpdateResultDto = new UserUpdateResultDto()
            {
                Id = Id,
                Name = Name,
                Email = Email
            };
        }
    }
}
