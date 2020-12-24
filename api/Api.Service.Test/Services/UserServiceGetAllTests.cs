using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Services
{
    public class UserServiceGetAllTests : UserServiceTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "GetAll method is avaliable")]

        public async Task TestGetAll()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll())
                .ReturnsAsync(listUserDto);

            _service = _serviceMock.Object;

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);


            var emptyUserDtoList = new List<UserDto>();

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll())
                .ReturnsAsync(emptyUserDtoList.AsEnumerable);

            _service = _serviceMock.Object;

            var emptyResult = await _service.GetAll();

            Assert.Empty(emptyResult);
            Assert.True(emptyResult.Count() == 0);
        }
    }
}