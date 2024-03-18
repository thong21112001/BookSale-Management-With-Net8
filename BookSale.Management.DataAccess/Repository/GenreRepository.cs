using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(BookSaleDbContext context) : base(context)
        {
        }

        public new IQueryable<Genre> Table => base.Table;

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            return await GetALlAsync(x => x.IsActive);
        }

        public async Task<Genre> GetById(int id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task CreateGenre(Genre genre)
        {
            await Create(genre);
        }

        public void UpdateGenre(Genre genre)
        {
           Update(genre);
        }

        public void DeleteGenre(Genre genre)
        {
            Delete(genre);
        }

        public async Task SaveGenre()
        {
            await Commit();
        }
	}
}
