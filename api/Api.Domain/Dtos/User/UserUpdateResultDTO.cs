using System;

namespace Api.Domain.Dtos.User
{
    public class UserUpdateResultDTO
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}