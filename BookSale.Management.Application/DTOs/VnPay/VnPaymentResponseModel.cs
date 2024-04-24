namespace BookSale.Management.Application.DTOs.VnPay
{
    public class VnPaymentResponseModel
    {
        public bool Success { get; set; }
        public string OrderDescription { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string VnPayResponseCode { get; set; } = string.Empty;   // (00) -> Success
    }
}
