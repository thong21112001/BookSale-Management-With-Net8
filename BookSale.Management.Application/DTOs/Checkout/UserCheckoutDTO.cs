using BookSale.Management.Domain.Enums;

namespace BookSale.Management.Application.DTOs.Checkout
{
    public class UserCheckoutDTO
    {
        public int Id { get; set; } = 0;

        public string Fullname { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string OrderId { get; set; } = string.Empty;

        public PaymentMethod PaymentMethod { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}
