namespace BookSale.Management.UI.Areas.Admin.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool HasRememberMe { get; set; }
    }
}
