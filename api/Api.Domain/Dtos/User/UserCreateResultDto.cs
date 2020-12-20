using System;

namespace Api.Domain.Dtos.User
{
    public class UserCreateResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}