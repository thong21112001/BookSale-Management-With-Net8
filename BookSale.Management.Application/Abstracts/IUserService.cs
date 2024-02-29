using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Abstracts
{
    public interface IUserService
    {
        Task<ResponseDataTable<UserModel>> GetAllUser(RequestDataTable request);
        Task<ResponseModel> Save(CreateAccountDTO request);
        Task<CreateAccountDTO> GetUserById(string id);
        Task<bool> Delete(string id);
    }
}