using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BookSale.Management.Application.DTOs.User
{
    public class UserProfileDTO
    {
        [ValidateNever]
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
