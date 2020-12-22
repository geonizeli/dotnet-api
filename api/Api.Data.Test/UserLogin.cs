using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using static Api.Data.Test.BaseTest;

namespace Api.Data.Test
{
    public class UserLogin : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
        public UserLogin(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "User Login")]
        public async Task Login()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var repository = new UserImplementation(context);
                var entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };
                var firstRegister = await repository.InsertAsync(entity);

                var login = await repository.FindByLoginAsync(firstRegister.Email);
                Assert.NotNull(login);
                Assert.Equal(firstRegister.Email, login.Email);
            }
        }
    }
}