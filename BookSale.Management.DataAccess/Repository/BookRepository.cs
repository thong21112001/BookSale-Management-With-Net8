using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using BookSale.Management.Domain.Entities;

namespace BookSale.Management.DataAccess.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly BookSaleDbContext _context;

        public BookRepository(BookSaleDbContext context) : base(context)
        {
        }
    }
}
