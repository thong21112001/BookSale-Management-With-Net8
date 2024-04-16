using Microsoft.EntityFrameworkCore;

namespace BookSale.Management.Domain.Abstracts
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGenreRepository GenreRepository { get; }
        IUserAddressRepository UserAddressRepository { get; }
        ICartRepository CartRepository { get; }
        IOrderRepository OrderRepository { get; }
        void Dispose();
        void Detach(object entity);
        Task SaveChangeAsync();
        DbSet<T> Table<T>() where T : class;
        Task BeginTransactionAsync();
        Task SaveTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
