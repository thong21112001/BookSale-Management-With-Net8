using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Abstracts
{
    public interface IUserAddressService
    {
        Task<ResponseModel> Save(UserProfileDTO request, string userId);
        Task<IEnumerable<UserProfileDTO>> GetAllListAddressUser(string id);
    }
}
