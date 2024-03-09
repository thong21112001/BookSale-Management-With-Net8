using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(BookSaleDbContext context) : base(context)
        {
        }

		public async Task<IEnumerable<Book>> GetAllBook()
		{
			return await GetALlAsync();
		}
	}
}
