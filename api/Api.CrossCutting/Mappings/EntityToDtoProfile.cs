using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDto, UserEntity>()
               .ReverseMap();

            CreateMap<UserCreateDto, UserEntity>()
                .ReverseMap();

            CreateMap<UserUpdateDto, UserEntity>()
                .ReverseMap();
        }
    }
}
