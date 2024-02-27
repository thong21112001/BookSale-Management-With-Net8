using Microsoft.AspNetCore.Http;

namespace BookSale.Management.Application.DTOs
{
    public class CreateAccountDTO
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
