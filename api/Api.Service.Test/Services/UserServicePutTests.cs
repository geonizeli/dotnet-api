using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Services
{
    public class UserServicePutTests : UserServiceTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Put method is avaliable")]
        public async Task PutTests()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Put(userUpdateDto))
                .ReturnsAsync(userUpdateResultDto);

            _service = _serviceMock.Object;

            var result = await _service.Put(userUpdateDto);

            Assert.NotNull(result);
            Assert.Equal(userUpdateResultDto.Email, result.Email);
            Assert.Equal(userUpdateResultDto.Name, result.Name);
        }
    }
}