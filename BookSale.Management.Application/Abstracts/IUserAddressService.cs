using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.User;

namespace BookSale.Management.Application.Abstracts
{
    public interface IUserAddressService
    {
        Task<ResponseModel> Save(UserProfileDTO request, string userId);
        Task<IEnumerable<UserProfileDTO>> GetAllListAddressUser(string id);
    }
}
