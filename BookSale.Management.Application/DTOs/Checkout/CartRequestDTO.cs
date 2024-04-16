using BookSale.Management.Application.DTOs.Book;
using BookSale.Management.Domain.Enums;

namespace BookSale.Management.Application.DTOs.Checkout
{
    public class CartRequestDTO
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public string Note { get; set; } = string.Empty;
        public StatusProcessing Status { get; set; }
        public bool IsActive { get; set; }
        public string? UserId { get; set; }
        public List<BookForCart> BookForCarts { get; set; }
    }
}
