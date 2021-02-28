using System;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class DtoToModelProfileTests : BaseTest
    {
        public UserModel _model { get; }
        public DtoToModelProfileTests() {
            _model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            };
        }

        [Fact(DisplayName = "User dto and model mapper's")]
        public void UserMappers()
        {
            var userDto = Mapper.Map<UserDto>(_model);
            var userCreateDto = Mapper.Map<UserCreateDto>(_model);
            var userCreateResultDto = Mapper.Map<UserCreateResultDto>(_model);
            var userUpdateDto = Mapper.Map<UserUpdateDto>(_model);
            var userUpdateResultDto = Mapper.Map<UserUpdateResultDto>(_model);

            // User Model to UserDto
            Assert.Equal(_model.Name, userDto.Name);
            Assert.Equal(_model.Email, userDto.Email);
            Assert.Equal(_model.Id, userDto.Id);
            Assert.Equal(_model.UpdatedAt, userDto.UpdatedAt);
            Assert.Equal(_model.CreatedAt, userDto.CreatedAt);

            // User Model to UserCreateDto
            Assert.Equal(_model.Name, userCreateDto.Name);
            Assert.Equal(_model.Email, userCreateDto.Email);

            // User Model to UserCreateResultDto
            Assert.Equal(_model.Name, userCreateResultDto.Name);
            Assert.Equal(_model.Email, userCreateResultDto.Email);
            Assert.Equal(_model.Id, userCreateResultDto.Id);
            Assert.Equal(_model.CreatedAt, userCreateResultDto.CreatedAt);

            // User Model to UserUpdateDto
            Assert.Equal(_model.Name, userUpdateDto.Name);
            Assert.Equal(_model.Email, userUpdateDto.Email);
            Assert.Equal(_model.Id, userUpdateDto.Id);

            // User Model to UserUpdateResultDto
            Assert.Equal(_model.Name, userUpdateResultDto.Name);
            Assert.Equal(_model.Email, userUpdateResultDto.Email);
            Assert.Equal(_model.Id, userUpdateResultDto.Id);
            Assert.Equal(_model.UpdatedAt, userUpdateResultDto.UpdatedAt);

            var userModel1 = Mapper.Map<UserModel>(userDto);
            var userModel2 = Mapper.Map<UserModel>(userCreateDto);
            var userModel3 = Mapper.Map<UserModel>(userCreateResultDto);
            var userModel4 = Mapper.Map<UserModel>(userUpdateDto);
            var userModel5 = Mapper.Map<UserModel>(userUpdateResultDto);

            // User UserDto to Model
            Assert.Equal(userModel1.Name, userDto.Name);
            Assert.Equal(userModel1.Email, userDto.Email);
            Assert.Equal(userModel1.Id, userDto.Id);
            Assert.Equal(userModel1.UpdatedAt, userDto.UpdatedAt);
            Assert.Equal(userModel1.CreatedAt, userDto.CreatedAt);

            // User UserCreateDto to Model
            Assert.Equal(userModel2.Name, userCreateDto.Name);
            Assert.Equal(userModel2.Email, userCreateDto.Email);

            // User UserCreateResultDto to Model
            Assert.Equal(userModel3.Name, userCreateResultDto.Name);
            Assert.Equal(userModel3.Email, userCreateResultDto.Email);
            Assert.Equal(userModel3.Id, userCreateResultDto.Id);
            Assert.Equal(userModel3.CreatedAt, userCreateResultDto.CreatedAt);

            // User UserUpdateDto to Model
            Assert.Equal(userModel4.Name, userUpdateDto.Name);
            Assert.Equal(userModel4.Email, userUpdateDto.Email);
            Assert.Equal(userModel4.Id, userUpdateDto.Id);

            // User UserUpdateResultDto to Model
            Assert.Equal(userModel5.Name, userUpdateResultDto.Name);
            Assert.Equal(userModel5.Email, userUpdateResultDto.Email);
            Assert.Equal(userModel5.Id, userUpdateResultDto.Id);
            Assert.Equal(userModel5.UpdatedAt, userUpdateResultDto.UpdatedAt);
        }
    }
}