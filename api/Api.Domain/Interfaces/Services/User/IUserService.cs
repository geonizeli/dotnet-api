using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Domain.Dtos.User;

namespace Api.Domain.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> Get(Guid id);
        Task<UserDto> Post(UserCreateDto user);
        Task<UserDto> Put(UserUpdateDto user);
        Task<bool> Delete(Guid id);
    }
}