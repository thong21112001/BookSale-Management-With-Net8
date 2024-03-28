namespace BookSale.Management.Application.DTOs
{
	//Phải đặt đúng thứ tự như trong store procedure Select
	public class BookDTO
	{
		public string? GenreName { get; set; }
		public int GenreId { get; set; }
		public string? Code { get; set; }
		public string? Title { get; set; }
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
