using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
    public interface IUserAddressRepository
    {
        Task CreateUserAddress(UserAddress userAddress);
        Task<IEnumerable<UserAddress>> GetAllListAddressUser(string id);
    }
}
