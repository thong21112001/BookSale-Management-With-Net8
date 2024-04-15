using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }
}
