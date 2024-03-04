using BookSale.Management.Domain.Entities;

namespace BookSale.Management.Domain.Abstracts
{
    public interface IGenreRepository
    {
        Task CreateGenre(Genre genre);
        void DeleteGenre(Genre genre);
        Task<IEnumerable<Genre>> GetAllGenre();
        Task<Genre> GetById(int id);
        Task SaveGenre();
        void UpdateGenre(Genre genre);
    }
}
