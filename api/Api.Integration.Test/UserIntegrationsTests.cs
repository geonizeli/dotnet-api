using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Service.Services;
using Newtonsoft.Json;
using Xunit;

namespace Api.Integration.Test
{
    public class UserIntegrationsTests : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        [Fact(DisplayName = "User CRUD integration test")]
        public async Task Crud()
        {
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            await AddToken();

            var userDto = new UserCreateDto()
            {
                Name = _name,
                Email = _email
            };

            // CREATE - POST
            var postResult = await PostJsonAsync(userDto, $"{hostApi}users", client);

            Assert.Equal(HttpStatusCode.Created, postResult.StatusCode);

            var postResultContent = await postResult.Content.ReadAsStringAsync();
            var userCreateResult = JsonConvert.DeserializeObject<UserDto>(postResultContent);

            Assert.Equal(_name, userCreateResult.Name);
            Assert.Equal(_email, userCreateResult.Email);
            Assert.True(userCreateResult.Id != default(Guid));

            // LIST - GET
            var getAllResult = await client.GetAsync($"{hostApi}users");

            Assert.Equal(HttpStatusCode.OK, getAllResult.StatusCode);

            var getAllResultContent = await getAllResult.Content.ReadAsStringAsync();
            var usersList = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(getAllResultContent);

            Assert.NotNull(usersList);
            Assert.True(usersList.Count() > 0);
            Assert.True(usersList.Where(r => r.Id == userCreateResult.Id).Count() == 1);

            var updateUserDto = new UserUpdateDto()
            {
                Id = userCreateResult.Id,
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email()
            };

            // UPDATE - PUT
            var stringContent = new StringContent(
                JsonConvert.SerializeObject(updateUserDto),
                Encoding.UTF8, "application/json"
            );

            var updateResult = await client.PutAsync($"{hostApi}users", stringContent);
            var updateResultContent = await updateResult.Content.ReadAsStringAsync();
            var updatedUser = JsonConvert.DeserializeObject<UserUpdateDto>(updateResultContent);

            Assert.Equal(HttpStatusCode.OK, updateResult.StatusCode);
            Assert.NotEqual(userCreateResult.Name, updatedUser.Name);
            Assert.NotEqual(userCreateResult.Email, updatedUser.Email);

            // GET Id
            response = await client.GetAsync($"{hostApi}users/{updatedUser.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var getedUser = JsonConvert.DeserializeObject<UserDto>(jsonResult);

            Assert.NotNull(getedUser);
            Assert.Equal(getedUser.Name, updatedUser.Name);
            Assert.Equal(getedUser.Email, updatedUser.Email);

            //DELETE
            var deleteResponse = await client.DeleteAsync($"{hostApi}users/{getedUser.Id}");
            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

            //GET ID depois do DELETE
            var getDeletedUserResponse = await client.GetAsync($"{hostApi}users/{getedUser.Id}");
            Assert.Equal(HttpStatusCode.NoContent, getDeletedUserResponse.StatusCode);
        }
    }
}
