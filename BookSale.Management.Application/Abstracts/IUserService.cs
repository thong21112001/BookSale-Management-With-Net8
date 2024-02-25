using BookSale.Management.Application.DTOs;

namespace BookSale.Management.Application.Abstracts
{
    public interface IUserService
    {
        Task<ResponseModel> CheckLogin(string username, string password, bool hasRememberMe);
        Task SignOut();
    }
}