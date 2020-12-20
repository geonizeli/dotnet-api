using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "is required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "is required")]
        [StringLength(30, ErrorMessage = "is too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "is not valid")]
        [StringLength(100, ErrorMessage = "is too long")]
        public string Email { get; set; }
    }
}