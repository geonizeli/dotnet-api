using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using static Api.Data.Test.BaseTest;

namespace Api.Data.Test
{
    public class UserCrud : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
        public UserCrud(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "User CRUD")]
        public async Task CRUD()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                // Create
                var repository = new UserImplementation(context);
                var entity = new UserEntity
                {
                    Email = Faker.Internet.Email(),
                    Name = Faker.Name.FullName()
                };
                var firstRegister = await repository.InsertAsync(entity);

                Assert.NotNull(firstRegister);
                Assert.Equal(entity.Email, firstRegister.Email);
                Assert.Equal(entity.Name, firstRegister.Name);
                Assert.True(firstRegister.Id != Guid.Empty);

                // Read
                var registerExits = await repository.ExistsAsync(firstRegister.Id);
                Assert.True(registerExits);

                var selectedRegister = await repository.SelectAsync(firstRegister.Id);
                Assert.NotNull(selectedRegister);
                Assert.Equal(selectedRegister.Email, firstRegister.Email);
                Assert.Equal(selectedRegister.Name, firstRegister.Name);

                var registerList = await repository.SelectAsync();
                Assert.NotNull(registerList);
                Assert.True(registerList.Count() > 0);

                // Update
                firstRegister.Name = Faker.Name.First();
                var updatedRegister = await repository.UpdateAsync(firstRegister);

                Assert.NotNull(updatedRegister);
                Assert.Equal(firstRegister.Email, updatedRegister.Email);
                Assert.Equal(firstRegister.Name, updatedRegister.Name);

                // Delete
                var registerHasDeleted = await repository.DeleteAsync(selectedRegister.Id);
                Assert.True(registerHasDeleted);
            }
        }
    }
}