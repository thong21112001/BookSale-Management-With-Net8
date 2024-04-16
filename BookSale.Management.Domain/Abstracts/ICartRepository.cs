using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
    public interface ICartRepository
    {
        Task Save(Cart cart);
    }
}
