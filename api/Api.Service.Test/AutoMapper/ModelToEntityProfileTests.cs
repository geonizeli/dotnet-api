using System;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Models;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class ModelToEntityProfileTests : BaseTest
    {
        UserModel _model;
        public ModelToEntityProfileTests()
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

        [Fact(DisplayName = "User model and entity mapper's")]
        public void ModelToEntityProfileTestsTests()
        {
            var entity = Mapper.Map<UserEntity>(_model);

            Assert.Equal(_model.Id, entity.Id);
            Assert.Equal(_model.Name, entity.Name);
            Assert.Equal(_model.Email, entity.Email);
            Assert.Equal(_model.UpdatedAt, entity.UpdatedAt);
            Assert.Equal(_model.CreatedAt, entity.CreatedAt);

            var model = Mapper.Map<UserModel>(entity);

            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Name, model.Name);
            Assert.Equal(entity.Email, model.Email);
            Assert.Equal(entity.UpdatedAt, model.UpdatedAt);
            Assert.Equal(entity.CreatedAt, model.CreatedAt);
        }
    }
}