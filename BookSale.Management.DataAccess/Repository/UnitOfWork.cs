using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using Microsoft.EntityFrameworkCore;

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

        //detach các entity ( tách các đối tượng entity ra khỏi context là
        //một cách để ngăn chặn sự theo dõi của context đối với đối tượng đó ).
        public void Detach(object entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
