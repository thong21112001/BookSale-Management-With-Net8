using BookSale.Management.DataAccess.DataAccess;
using BookSale.Management.Domain.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookSale.Management.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BookSaleDbContext _context;
		private readonly ISQLQueryHandler _queryHandler;
		IGenreRepository? _genreRepository;
        IBookRepository? _bookRepository;
        IUserAddressRepository? _addressRepository;
        ICartRepository? _cartRepository;
        IOrderRepository? _orderRepository;
        IDbContextTransaction _contextTransaction;
        private bool disposedValue;

        public UnitOfWork(BookSaleDbContext context, ISQLQueryHandler queryHandler)
        {
            _context = context;
			_queryHandler = queryHandler;
		}
        public DbSet<T> Table<T>() where T : class => _context.Set<T>();
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context);
        public IBookRepository BookRepository => _bookRepository ??= new BookRepository(_context,_queryHandler);
        public IUserAddressRepository UserAddressRepository => _addressRepository ??= new UserAddressRepository(_context);
        public ICartRepository CartRepository => _cartRepository ??= new CartRepository(_context);
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_context, _queryHandler);


        public async Task BeginTransactionAsync()
        {
            _contextTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task SaveTransactionAsync()
        {
            await _contextTransaction?.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _contextTransaction?.RollbackAsync();
        }

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
