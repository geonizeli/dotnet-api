using System;
using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Services
{
    public class UserServiceDeleteTests : UserServiceTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Delete method is avaliable")]
        public async Task PutTests()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(Id))
                .ReturnsAsync(true);

            _service = _serviceMock.Object;

            var success = await _service.Delete(Id);

            Assert.True(success);

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            _service = _serviceMock.Object;

            var fail = await _service.Delete(Guid.NewGuid());

            Assert.False(fail);
        }
    }
}