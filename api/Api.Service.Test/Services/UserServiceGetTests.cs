using System;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Services
{
    public class UserServiceGetTests : UserServiceTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Get method is avaliable")]
        public async Task TestGet()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(Id))
                .ReturnsAsync(userDto);

            _service = _serviceMock.Object;

            var result = await _service.Get(Id);

            Assert.NotNull(result);
            Assert.Equal(Id, result.Id);
            Assert.Equal(Name, result.Name);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).Returns(Task.FromResult((UserDto) null));

            _service = _serviceMock.Object;

            var record = await _service.Get(Id);
            Assert.Null(record);
        }
    }
}