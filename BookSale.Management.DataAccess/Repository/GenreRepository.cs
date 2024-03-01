using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        private readonly BookSaleDbContext _context;

        public GenreRepository(BookSaleDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Genre>> GetAllGenre()
        {
            return await GetALlAsync();
        }

	}
}
