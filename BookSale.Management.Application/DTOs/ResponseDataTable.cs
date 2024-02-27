namespace BookSale.Management.Application.DTOs
{
    public class ResponseDataTable<T>
    {
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public int Draw { get; set; }
        public List<T> Data { get; set; }
    }
}
