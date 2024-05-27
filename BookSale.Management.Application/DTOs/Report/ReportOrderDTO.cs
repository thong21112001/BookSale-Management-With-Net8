namespace BookSale.Management.Application.DTOs.Report
{
	public class ReportOrderDTO
	{
        public string Code { get; set; } = string.Empty;
        public DateTime CreateOn { get; set; }
        public OrderAddressDTO Address { get; set; }
        public IEnumerable<OrderDetailDTO> Details { get; set; }
    }

    public class OrderAddressDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class OrderDetailDTO
    {
        public string ProductName { get; set; } = string.Empty;
		public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
