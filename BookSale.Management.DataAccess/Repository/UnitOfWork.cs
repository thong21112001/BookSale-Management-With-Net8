using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;

namespace BookSale.Management.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BookSaleDbContext _context;
		private readonly ISQLQueryHandler _queryHandler;
		IGenreRepository? _genreRepository;
        IBookRepository? _bookRepository;
        private bool disposedValue;

        public UnitOfWork(BookSaleDbContext context, ISQLQueryHandler queryHandler)
        {
            _context = context;
			_queryHandler = queryHandler;
		}

        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context,_queryHandler);


        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }

        //Tránh tình trạng tràn bộ nhớ
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

       
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
