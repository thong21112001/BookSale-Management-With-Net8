using BookSale.Management.Application.DTOs;
using BookSale.Management.Application.DTOs.AuthenticationUser;
using BookSale.Management.Application.DTOs.ViewModels;

namespace BookSale.Management.Application.Abstracts
{
    public interface IUserService
    {
        Task<ResponseDataTable<UserModel>> GetAllUser(RequestDataTable request);
        Task<ResponseModel> Save(CreateAccountDTO request);
        Task<CreateAccountDTO> GetUserById(string id);
        Task<bool> Delete(string id);
        Task<UserProfileDTO> GetUserProfileDTO(string id);
        Task<UserProfileViewModel> GetUserProfileViewModel(string id);
        Task<bool> UpdateProfileUser(UserProfileViewModel userProfileVM, string id);
        Task<bool> RegisterAsync(UserRegisterDTO registerDTO);
    }
}