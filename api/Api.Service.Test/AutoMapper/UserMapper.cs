using System;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UserMapper : BaseTest
    {
        UserModel _model;
        public UserMapper()
        {
            _model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            };

        }

        [Fact(DisplayName = "User model mapper's")]
        public void UserMapperTests()
        {
            var entity = Mapper.Map<UserEntity>(_model);

            Assert.Equal(_model.Id, entity.Id);
            Assert.Equal(_model.Name, entity.Name);
            Assert.Equal(_model.Email, entity.Email);
            Assert.Equal(_model.UpdatedAt, entity.UpdatedAt);
            Assert.Equal(_model.CreatedAt, entity.CreatedAt);

            var dto = Mapper.Map<UserDto>(entity);

            Assert.Equal(entity.Id, dto.Id);
            Assert.Equal(entity.Name, dto.Name);
            Assert.Equal(entity.Email, dto.Email);
            Assert.Equal(entity.UpdatedAt, dto.UpdatedAt);
            Assert.Equal(entity.CreatedAt, dto.CreatedAt);
        }
    }
}