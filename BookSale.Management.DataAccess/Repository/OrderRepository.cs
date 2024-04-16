using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(BookSaleDbContext context) : base(context)
        {
            
        }

        public async Task Save(Order order)
        {
            await base.Create(order);
        }
    }
}
