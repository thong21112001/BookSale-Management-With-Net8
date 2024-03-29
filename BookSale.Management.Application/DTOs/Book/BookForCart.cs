namespace BookSale.Management.Application.DTOs.Book
{
    public class BookForCart
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Code { get; set; } = string.Empty;
        public double Total { get; set; }
    }
}
