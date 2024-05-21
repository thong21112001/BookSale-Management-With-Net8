using BookSale.Management.Domain.Enums;

namespace BookSale.Management.Application.DTOs.Order
{
	public class OrderDTO
	{
        public string Id { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public StatusProcessing Status { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public double TotalPrice { get; set; }
    }
}
