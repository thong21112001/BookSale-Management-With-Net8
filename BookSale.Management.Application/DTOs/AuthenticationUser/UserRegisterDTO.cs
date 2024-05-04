using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs.AuthenticationUser
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Username không bỏ trống")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password không bỏ trống")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không bỏ trống")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone không bỏ trống")]
        public string? Phone { get; set; }
    }
}
