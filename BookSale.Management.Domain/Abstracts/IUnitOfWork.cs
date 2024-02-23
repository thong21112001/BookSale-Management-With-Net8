namespace BookSale.Management.Domain.Abstracts
{
    public interface IUnitOfWork
    {
        IBookRepository BookRepository { get; }
        IGenreRepository GenreRepository { get; }


        Task SaveChangeAsync();
    }
}
