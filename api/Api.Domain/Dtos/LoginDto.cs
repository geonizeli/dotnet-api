using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "is required to Login")]
        [EmailAddress(ErrorMessage = "is not valid")]
        [StringLength(100, ErrorMessage = "is too long")]
        public string Email { get; set; }
    }
}