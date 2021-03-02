using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Controllers
{
    public class UsersControllerTests
    {
        private UsersController _controller;

        [Fact(DisplayName = "Successful on list all user")]
        public async Task ListUsers()
        {
            var iUserServiceMock = new Mock<IUserService>();

            iUserServiceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<UserDto>
                {
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        UpdatedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    },
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        UpdatedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    }
                }
            );

            _controller = new UsersController(iUserServiceMock.Object);

            var result = await _controller.GetAll();

            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value as List<UserDto>;

            Assert.True(resultValue is List<UserDto>);
            Assert.Equal(2, resultValue.Count);
        }

        [Fact(DisplayName = "Bad request on list all user")]
        public async Task ListUsersBadRequest()
        {
            var iUserServiceMock = new Mock<IUserService>();

            iUserServiceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<UserDto>
                {
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        UpdatedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    },
                    new UserDto
                    {
                        Id = Guid.NewGuid(),
                        Name = Faker.Name.FullName(),
                        Email = Faker.Internet.Email(),
                        UpdatedAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow
                    }
                }
            );

            _controller = new UsersController(iUserServiceMock.Object);
            _controller.ModelState.AddModelError("Test", "test");
            var result = await _controller.GetAll();

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact(DisplayName = "Successful on read a user")]
        public async Task ReadUser()
        {
            var iUserServiceMock = new Mock<IUserService>();
            var id = Guid.NewGuid();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            iUserServiceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UserDto
                {
                    Id = id,
                    Name = name,
                    Email = email,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(iUserServiceMock.Object);

            var result = await _controller.Get(id);

            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value as UserDto;

            Assert.NotNull(resultValue);
            Assert.Equal(id, resultValue.Id);
            Assert.Equal(name, resultValue.Name);
            Assert.Equal(email, resultValue.Email);
        }

        [Fact(DisplayName = "Bad request on read a user")]
        public async Task ReadUserBadRequest()
        {
            var iUserServiceMock = new Mock<IUserService>();
            var id = Guid.NewGuid();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            iUserServiceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UserDto
                {
                    Id = id,
                    Name = name,
                    Email = email,
                    CreatedAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(iUserServiceMock.Object);
            _controller.ModelState.AddModelError("Id", "is invalid");

            var result = await _controller.Get(id);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact(DisplayName = "Successful on create")]
        public async Task CreateUserSuccess()
        {
            var iUserServiceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            iUserServiceMock.Setup(m => m.Post(It.IsAny<UserCreateDto>())).ReturnsAsync(
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    CreatedAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(iUserServiceMock.Object);

            var url = new Mock<IUrlHelper>();
            url.Setup(m => m.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            _controller.Url = url.Object;

            var userCreateDto = new UserCreateDto
            {
                Name = name,
                Email = email
            };

            var result = await _controller.Post(userCreateDto);

            Assert.True(result is CreatedResult);

            var resultValues = ((CreatedResult) result).Value as UserDto;

            Assert.NotNull(resultValues);
            Assert.Equal(name, resultValues.Name);
            Assert.Equal(email, resultValues.Email);
        }

        [Fact(DisplayName = "Bad request on create")]
        public async Task CreateUserBadRequest()
        {
            var iUserServiceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            iUserServiceMock.Setup(m => m.Post(It.IsAny<UserCreateDto>())).ReturnsAsync(
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    CreatedAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(iUserServiceMock.Object);
            _controller.ModelState.AddModelError("Name", "is required");

            var url = new Mock<IUrlHelper>();
            url.Setup(m => m.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            _controller.Url = url.Object;

            var userCreateDto = new UserCreateDto
            {
                Name = name,
                Email = email
            };

            var result = await _controller.Post(userCreateDto);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact(DisplayName = "Successful on update")]
        public async Task UpdateUser()
        {
            var iUserServiceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            iUserServiceMock.Setup(m => m.Put(It.IsAny<UserUpdateDto>())).ReturnsAsync(
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(iUserServiceMock.Object);

            var userUpdateDto = new UserUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email
            };

            var result = await _controller.Put(userUpdateDto);

            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value as UserDto;

            Assert.NotNull(resultValue);
            Assert.Equal(email, resultValue.Email);
            Assert.Equal(name, resultValue.Name);
        }

        [Fact(DisplayName = "Bad request on update")]
        public async Task UpdateUserBadRequest()
        {
            var iUserServiceMock = new Mock<IUserService>();
            var name = Faker.Name.FullName();
            var email = Faker.Internet.Email();

            iUserServiceMock.Setup(m => m.Put(It.IsAny<UserUpdateDto>())).ReturnsAsync(
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Email = email,
                    UpdatedAt = DateTime.UtcNow
                }
            );

            _controller = new UsersController(iUserServiceMock.Object);
            _controller.ModelState.AddModelError("Name", "is required");

            var userUpdateDto = new UserUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email
            };

            var result = await _controller.Put(userUpdateDto);

            Assert.True(result is BadRequestObjectResult);
        }

        [Fact(DisplayName = "Successful on delate")]
        public async Task DeleteUser()
        {
            var iUserServiceMock = new Mock<IUserService>();

            iUserServiceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(iUserServiceMock.Object);

            var result = await _controller.Delete(Guid.NewGuid());

            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult) result).Value;

            Assert.True((Boolean) resultValue);
        }

        [Fact(DisplayName = "Bad request on delate")]
        public async Task DeleteUserBadRequest()
        {
            var iUserServiceMock = new Mock<IUserService>();

            iUserServiceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(iUserServiceMock.Object);
            _controller.ModelState.AddModelError("Id", "invalid format");

            var result = await _controller.Delete(default(Guid));

            Assert.True(result is BadRequestObjectResult);
        }
    }
}
