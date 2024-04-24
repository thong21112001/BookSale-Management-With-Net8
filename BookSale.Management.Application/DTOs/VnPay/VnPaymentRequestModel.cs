namespace BookSale.Management.Application.DTOs.VnPay
{
    public class VnPaymentRequestModel
    {
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
