namespace BookSale.Management.Domain.Abstracts
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGenreRepository GenreRepository { get; }

        void Dispose();
        void Detach(object entity);
        Task SaveChangeAsync();
    }
}
