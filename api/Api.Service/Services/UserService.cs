using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _repository;
        private IMapper _mapper;
        public UserService(IRepository<UserEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<UserDto> Get(Guid id)
        {
            var entity = await _repository.SelectAsync(id);

            return _mapper.Map<UserDto>(entity);
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var entityList = await _repository.SelectAsync();

            return _mapper.Map<IEnumerable<UserDto>>(entityList);
        }
        public async Task<UserCreateResultDto> Post(UserDto user)
        {
            var entity = _mapper.Map<UserEntity>(user);
            var entityResult = await _repository.InsertAsync(entity);

            return _mapper.Map<UserCreateResultDto>(entityResult);
        }
        public async Task<UserUpdateResultDto> Put(UserDto user)
        {
            var entity = _mapper.Map<UserEntity>(user);
            var entityResult = await _repository.UpdateAsync(entity);

            return _mapper.Map<UserUpdateResultDto>(entityResult);
        }
    }
}