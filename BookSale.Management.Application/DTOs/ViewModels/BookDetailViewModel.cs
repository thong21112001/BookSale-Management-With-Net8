namespace BookSale.Management.Application.DTOs.ViewModels
{
    public class BookDetailViewModel
    {
        public int GenreId { get; set; }
        public string? Code { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Available { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public string? Image { get; set; }
        public string? Publisher { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Id { get; set; }
    }
}
