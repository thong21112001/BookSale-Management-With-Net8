using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;

namespace BookSale.Management.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BookSaleDbContext _context;

        private IGenreRepository? _genreRepository;
        private IBookRepository? _bookRepository;

        public UnitOfWork(BookSaleDbContext context)
        {
            _context = context;
        }

        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context);



        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }


        //Tránh tình trạng tràn bộ nhớ
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
