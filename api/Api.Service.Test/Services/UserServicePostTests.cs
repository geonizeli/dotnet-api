using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Services
{
    public class UserServicePostTests : UserServiceTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "Post method is avaliable")]
        private async Task TestPost()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(userCreateDto))
                .ReturnsAsync(userCreateResultDto);

            _service = _serviceMock.Object;

            var result = await _service.Post(userCreateDto);

            Assert.NotNull(result);
            Assert.Equal(userCreateDto.Email, result.Email);
            Assert.Equal(userCreateDto.Name, result.Name);
        }
    }
}