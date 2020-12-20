using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos.User
{
    public class UserDTO
    {
        [Required(ErrorMessage = "is required to Login")]
        [StringLength(30, ErrorMessage = "is too long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "is required to Login")]
        [EmailAddress(ErrorMessage = "is not valid")]
        [StringLength(100, ErrorMessage = "is too long")]
        public string Email { get; set; }
    }
}