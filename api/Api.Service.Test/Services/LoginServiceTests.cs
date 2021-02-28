using System;
using System.Threading.Tasks;
using Api.Domain.Dtos;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;
namespace Api.Service.Test.Services
{
    public class LoginServiceTests : UserServiceTests
    {
        private ILoginService _service;
        private Mock<ILoginService> _serviceMock;

        [Fact(DisplayName = "Login is avaliable")]
        public async Task LoginTests()
        {

            var loginDto = new LoginDto()
            {
                Email = Faker.Internet.Email()
            };

            var response = new LoginResultDto
            {
                authenticated = true,
                create = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = DateTime.UtcNow.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = Guid.NewGuid().ToString(),
                userName = loginDto.Email,
                message = "Authentication successful",
            };

            _serviceMock = new Mock<ILoginService>();
            _serviceMock.Setup(m => m.FindByLoginAsync(loginDto))
                .ReturnsAsync(response);
            _service = _serviceMock.Object;

            var result = await _service.FindByLoginAsync(loginDto);

            Assert.NotNull(result);
            Assert.Equal(response, result);
        }
    }
}