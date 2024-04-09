namespace BookSale.Management.Domain.Abstracts
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGenreRepository GenreRepository { get; }
        IUserAddressRepository UserAddressRepository { get; }
        void Dispose();
        void Detach(object entity);
        Task SaveChangeAsync();
    }
}
