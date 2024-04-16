using BookSale.Management.Application.DTOs.Checkout;

namespace BookSale.Management.Application.Abstracts
{
    public interface ICartService
    {
        Task<bool> Save(CartRequestDTO requestDTO);
    }
}
