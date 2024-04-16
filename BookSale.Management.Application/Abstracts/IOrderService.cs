using BookSale.Management.Application.DTOs.Checkout;

namespace BookSale.Management.Application.Abstracts
{
    public interface IOrderService
    {
        Task<bool> Save(OrderRequestDTO requestDTO);
    }
}
