namespace BookSale.Management.Application.Abstracts
{
    public interface IUserService
    {
        Task<bool> CheckLogin(string username, string password);
        Task SignOut();
    }
}