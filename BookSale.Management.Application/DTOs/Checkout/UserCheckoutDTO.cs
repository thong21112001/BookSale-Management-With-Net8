using BookSale.Management.Domain.Enums;
using System.Text.Json.Serialization;

namespace BookSale.Management.Application.DTOs.Checkout
{
    public class UserCheckoutDTO
    {
        [JsonPropertyName("Address")]
        public string Address { get; set; } = string.Empty;

        [JsonPropertyName("Email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("Fullname")]
        public string Fullname { get; set; } = string.Empty;

        [JsonPropertyName("PaymentMethod")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonPropertyName("PhoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        [JsonPropertyName("TotalAmount")]
        public double TotalAmount { get; set; }
    }
}
