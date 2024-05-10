namespace BookSale.Management.Domain.Settings
{
    public class SmtpSettings   //Dùng để ánh xạ từ appsetting.json "EmailHost"
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string EmailUsername { get; set; } = string.Empty;
        public string EmailPassword { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
