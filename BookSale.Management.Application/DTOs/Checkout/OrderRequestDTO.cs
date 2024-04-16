using BookSale.Management.Application.DTOs.Book;
using BookSale.Management.Domain.Enums;

namespace BookSale.Management.Application.DTOs.Checkout
{
    public class OrderRequestDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public double TotalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string UserId { get; set; } = string.Empty;
        public StatusProcessing Status { get; set; }
        public List<BookForCart> BookForCarts { get; set; }
    }
}
