namespace BookSale.Management.Application.DTOs.Book
{
    public class BookForSiteDTO
    {
        public int totalRecords { get; set; }
        public int currentRecords { get; set; }
        public bool isDisableButton { get; set; }
        public double progessingValue { get; set; }
        public IEnumerable<BookDTO> Books { get; set; }
    }
}
