using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public class UserAddressRepository : GenericRepository<UserAddress>, IUserAddressRepository
    {
        public UserAddressRepository(BookSaleDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UserAddress>> GetAllListAddressUser(string id)
        {
            return await base.GetALlAsync(x => x.UserId == id);
        }

        public async Task CreateUserAddress(UserAddress userAddress)
        {
            await Create(userAddress);
        }
    }
}
