using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(BookSaleDbContext context) : base(context)
        {
            
        }

        public async Task Save(Cart cart)
        {
            if (cart.Id == 0)
            {
                await base.Create(cart);
            }
            else
            {
                base.Update(cart);
            }
        }
    }
}
