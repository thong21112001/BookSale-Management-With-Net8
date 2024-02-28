using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookSale.Management.Application.DTOs
{
    public class CreateAccountDTO
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Xin chọn RoleName")]
        public string? RoleName { get; set; }

        [Required(ErrorMessage = "Username không bỏ trống")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Fullname không bỏ trống")]
        public string? Fullname { get; set; }

        [Required(ErrorMessage = "Password không bỏ trống")]
        [MinLength(6, ErrorMessage = "Password từ 6 ký tự")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Email không bỏ trống")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone không bỏ trống")]
        public string? Phone { get; set; }

        public string? MobilePhone { get; set; }

        [Required(ErrorMessage = "Address không bỏ trống")]
        public string? Address { get; set; }

        public bool IsActive { get; set; }

        public IFormFile? Avatar { get; set; }
    }
}
