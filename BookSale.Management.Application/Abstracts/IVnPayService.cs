using BookSale.Management.Application.DTOs.VnPay;
using Microsoft.AspNetCore.Http;

namespace BookSale.Management.Application.Abstracts
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model);
        VnPaymentResponseModel PaymentExcute(IQueryCollection collection);
    }
}
