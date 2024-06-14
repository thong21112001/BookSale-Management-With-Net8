namespace BookSale.Management.Application.DTOs.ViewModels
{
    //Dùng để login google
    public class ExternalLoginModel
    {
        public string Provider { get; set; } = string.Empty;
        public string ReturnUrl { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
