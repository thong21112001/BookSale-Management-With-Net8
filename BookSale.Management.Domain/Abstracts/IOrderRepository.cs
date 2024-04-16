using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
    public interface IOrderRepository
    {
        Task Save(Order order);
    }
}
