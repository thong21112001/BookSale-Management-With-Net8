using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenre();
        Task<Genre> GetById(int id);
    }
}
